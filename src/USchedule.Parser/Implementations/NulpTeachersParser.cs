using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using USchedule.Parser.Base;
using USchedule.Parser.Executor;
using USchedule.Shared.Models;

namespace USchedule.Parser.Implementations
{
    public class NulpTeachersParser : BaseParser
    {
        private readonly string _apiUrl;
        private ReaderWriterLockSlim _storageLock = new ReaderWriterLockSlim();
        private volatile int _jobCount = 0;

        private ConcurrentBag<DepartmentSharedModel> _storage = new ConcurrentBag<DepartmentSharedModel>();

//        private IList<DepartmentSharedModel> _storage = new List<DepartmentSharedModel>();

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
                _jobCount++;
                _storageLock.ExitWriteLock();
                yield return new ParseTask(TeacherTask, $"?kaf={departmentId}", taskArgs);
            }
        }

        private IEnumerable<ParseTask> TeacherTask(HtmlDocument document, Dictionary<string, string> taskArgs)
        {
            var teachers = document.DocumentNode.SelectNodes("//select[@name='fnsn']/option");
            var department = new DepartmentSharedModel {Name = taskArgs[ConstKeys.DepartmentName]};
            var teachersList = new List<TeacherSharedModel>();
            foreach (var teacher in teachers)
            {
                if (string.IsNullOrEmpty(teacher.InnerText))
                {
                    continue;
                }

                Logger.LogInformation(
                    $"Parsed teacher {teacher.InnerText} with department {taskArgs[ConstKeys.DepartmentName]}");

                var teacherName = teacher.InnerText.Split(" ");
                var teacherModel = new TeacherSharedModel
                {
                    FirstName = teacherName[0],
                    LastName = teacherName[1]
                };
                teachersList.Add(teacherModel);
            }

            department.Teachers = teachersList;
            _storage.Add(department);
            _storageLock.EnterWriteLock();
            _jobCount--;
            if (_jobCount == 0)
            {
                Task.Run(async () => await PostDataToServer(_storage.ToList()));
            }

            _storageLock.ExitWriteLock();

            yield break;
        }

        private async Task PostDataToServer(IList<DepartmentSharedModel> department)
        {
            var httpClient = HttpClientFactory.Create();
            try
            {
                var response = await httpClient.PostAsJsonAsync(_apiUrl, department);
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