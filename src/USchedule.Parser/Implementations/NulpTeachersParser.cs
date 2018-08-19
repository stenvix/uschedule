using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using USchedule.Parser.Base;
using USchedule.Parser.Executor;
using USchedule.Shared.Models;

namespace USchedule.Parser
{
    public class NulpTeachersParser : BaseParser
    {
        private readonly string _apiUrl;
        private ReaderWriterLockSlim _storageLock = new ReaderWriterLockSlim();
        private volatile int _jobCount = 0;
        private ConcurrentBag<DepartmentSharedModel> _storage = new ConcurrentBag<DepartmentSharedModel>();

        public NulpTeachersParser(string baseUrl, string apiUrl, ILogger<BaseParser> logger,
            ILogger<ParseJob> parseLogger) : base(baseUrl, logger, parseLogger)
        {
            _apiUrl = apiUrl;
        }

        protected override IEnumerable<ParseTask> InitialTask(HtmlDocument document)
        {
            var departments = document.DocumentNode.SelectNodes("//select[@name='kaf']/option");
            var filePath = Path.Join(Directory.GetCurrentDirectory(), "department.txt");
            var dict = departments.Where(i => !string.IsNullOrEmpty(i.InnerText))
                .ToDictionary(i => i.InnerText, i => i.InnerLength.ToString());


            File.WriteAllText(filePath, JsonConvert.SerializeObject(dict));

            foreach (var department in departments)
            {
                if (string.IsNullOrEmpty(department.InnerText))
                {
                    continue;
                }

                Logger.LogInformation($"Parsed department {department.InnerText}");
                var departmentId = department.GetAttributeValue("value", String.Empty);

                var taskArgs = new Dictionary<string, string>
                {
                    {ConstKeys.DepartmentName, department.InnerText},
                    {ConstKeys.DepartmentId, departmentId}
                };

                _storageLock.EnterWriteLock();
                _storage.Add(new DepartmentSharedModel
                {
                    Name = department.InnerText.Trim(),
                    Teachers = new List<TeacherSharedModel>()
                });
                _storageLock.ExitWriteLock();

                yield return new ParseTask(TeacherTask, $"?kaf={departmentId}", taskArgs);
            }
        }

        private IEnumerable<ParseTask> TeacherTask(HtmlDocument document, Dictionary<string, string> taskArgs)
        {
            var teachers = document.DocumentNode.SelectNodes("//select[@name='fnsn']/option");

            foreach (var teacher in teachers)
            {
                if (string.IsNullOrEmpty(teacher.InnerText))
                {
                    continue;
                }

                Logger.LogInformation(
                    $"Parsed teacher {teacher.InnerText} with department {taskArgs[ConstKeys.DepartmentName]}");

                var teacherName = teacher.InnerText.Split(" ");


                var args = new Dictionary<string, string>(taskArgs)
                {
                    [ConstKeys.TeacherLastName] = teacherName[0].Trim(),
                    [ConstKeys.TeacherFirstName] = teacherName[1].Trim(),
                };
                _storageLock.EnterWriteLock();
                _jobCount++;
                _storageLock.ExitWriteLock();
                yield return new ParseTask(SubjectTask,
                    $"?kaf={taskArgs[ConstKeys.DepartmentId]}&fnsn={HttpUtility.UrlEncode(teacher.InnerHtml)}", args);
            }
        }

        private IEnumerable<ParseTask> SubjectTask(HtmlDocument document, Dictionary<string, string> taskArgs)
        {
            Logger.LogInformation(
                $"Parsed teacher subjects {taskArgs[ConstKeys.TeacherLastName]} {taskArgs[ConstKeys.TeacherFirstName]} with department {taskArgs[ConstKeys.DepartmentName]}");
            var div = document.DocumentNode.SelectSingleNode("//div[@id='vykl']");
            var rows = document.DocumentNode.SelectNodes("//div[@id='vykl']/div/table/tr");

            var teacherSubjects = new HashSet<string>();
            foreach (var row in rows)
            {
                var columns = row.SelectNodes("./td");
                if (columns == null || columns.Count < 2)
                {
                    continue;
                }

                ParseSubject(columns.Last(), teacherSubjects);
            }

            var teacherModel = new TeacherSharedModel
            {
                FirstName = taskArgs[ConstKeys.TeacherFirstName],
                LastName = taskArgs[ConstKeys.TeacherLastName],
                Subjects = teacherSubjects.Select(i => new TeacherSubjectSharedModel {Title = i}).ToList()
            };

            _storageLock.EnterWriteLock();
            var department = _storage.First(i => i.Name == taskArgs[ConstKeys.DepartmentName]);
            department.Teachers.Add(teacherModel);
            Logger.LogInformation($"Job {_jobCount} finished");
            _jobCount--;
            if (_jobCount == 0)
            {
                Task.Run(() => PostDataToServer(_storage.ToList()));
            }

            _storageLock.ExitWriteLock();

            yield break;
        }

        private void ParseSubject(HtmlNode html, HashSet<string> subjects)
        {
            var weekRows = html.SelectNodes("./table/tr");
            foreach (var weekRow in weekRows)
            {
                var subgroups = weekRow.SelectNodes("./td");
                foreach (var subgroup in subgroups)
                {
                    var subject = subgroup.SelectSingleNode("./div");
                    if (subject != null)
                    {
                        var subjecTitle = subject.SelectSingleNode("./b").InnerHtml.Trim();
                        if (!subjects.Contains(subjecTitle))
                        {
                            subjects.Add(subjecTitle);
                        }
                    }
                }
            }
        }

        public async Task PostDataToServer(IList<DepartmentSharedModel> departments, bool saveToFile = true)
        {
            var httpClient = HttpClientFactory.Create();
            try
            {
                var path = Path.Join(Directory.GetCurrentDirectory(), "teachers.json");
                if (saveToFile)
                {
                    await File.WriteAllTextAsync(path, JsonConvert.SerializeObject(departments));
                }
                Logger.LogInformation($"Posting data to {_apiUrl}");
                var response = await httpClient.PostAsJsonAsync(_apiUrl, departments);
                if (response.IsSuccessStatusCode)
                {
                    Logger.LogInformation($"Data successfully posted to server");
                }
                else
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    Logger.LogError($"Failed to post data: {responseMessage}");
                }
            }
            catch (Exception e)
            {
                Logger.LogError($"Error occured while posting data: {e.Message}", e);
            }
        }
    }
}