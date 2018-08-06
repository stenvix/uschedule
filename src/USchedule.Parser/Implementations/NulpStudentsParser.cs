using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using USchedule.Parser.Base;
using USchedule.Parser.Executor;
using USchedule.Shared.Enums;
using USchedule.Shared.Models;

namespace USchedule.Parser
{
    public class NulpStudentsParser: BaseParser
    {
        private readonly string _apiUrl;

        public NulpStudentsParser(string baseUrl, string apiUrl, ILogger<NulpStudentsParser> logger, ILogger<ParseJob> parseLogger):base(baseUrl, logger, parseLogger)
        {
            _apiUrl = apiUrl;
        }
        
        private static Dictionary<string, DayOfWeek> WeekDays = new Dictionary<string, DayOfWeek>
        {
            {"Пн", DayOfWeek.Monday},{"Вт", DayOfWeek.Tuesday}, {"Ср", DayOfWeek.Wednesday},
            {"Чт", DayOfWeek.Thursday}, {"Пт", DayOfWeek.Friday}, {"Сб", DayOfWeek.Saturday}, {"Нд", DayOfWeek.Sunday } 
        };
        
        public static Dictionary<string, SubjectTypeShared> SubjectTypes = new Dictionary<string, SubjectTypeShared>
        {
            {"лаб.", SubjectTypeShared.Lab}, {"прак.", SubjectTypeShared.Practical}, {"лекція", SubjectTypeShared.Lecture}
        };

        protected override IEnumerable<ParseTask> InitialTask(HtmlDocument document)
        {
            var institutes = document.DocumentNode.SelectNodes("//select[@name='inst']/option");
            var semester = document.DocumentNode.SelectSingleNode("//select[@name='semestr']/option[@selected]");
            var semesterId = semester.GetAttributeValue("value", string.Empty);
            var semesterPart = document.DocumentNode.SelectSingleNode("//select[@name='semest_part']/option[@selected]");
            var semesterPartId = semesterPart.GetAttributeValue("value", String.Empty);
            
            foreach (var institute in institutes)
            {
                var taskArgs = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(institute.InnerText) || string.IsNullOrEmpty(semesterId)|| string.IsNullOrEmpty(semesterPartId))
                {
                    continue;
                }
                
                var instituteId = institute.GetAttributeValue("value", string.Empty);
                
                taskArgs.Add(ConstKeys.InstituteName, institute.InnerHtml);
                taskArgs.Add(ConstKeys.InstituteId, instituteId);
                taskArgs.Add(ConstKeys.SemesterId, semesterId);
                taskArgs.Add(ConstKeys.SemesterPartId, semesterPartId);

                Logger.LogInformation($"Parsed institute: {institute.InnerText}");
                
                yield return new ParseTask(InstituteTask, $"?inst={instituteId}&group=&semestr={semesterId}&semest_part={semesterPartId}", taskArgs);
            }
        }

        private IEnumerable<ParseTask> InstituteTask(HtmlDocument document, Dictionary<string, string> taskArgs)
        {
            Logger.LogInformation($"Parse groups from institute {taskArgs[ConstKeys.InstituteName]}");
            
            var groups = document.DocumentNode.SelectNodes("//select[@name='group']/option");

            foreach (var group in groups)
            {
                var nextTaskArgs = new Dictionary<string, string>(taskArgs);
                var groupId = group.GetAttributeValue("value", string.Empty);
                if (string.IsNullOrEmpty(group.InnerText) || string.IsNullOrEmpty(groupId))
                {
                    continue;
                }

                nextTaskArgs[ConstKeys.GroupId] = groupId;
                nextTaskArgs[ConstKeys.GroupName] = group.InnerText;
                
                yield return new ParseTask(ScheduleTask, $"?inst={taskArgs[ConstKeys.InstituteId]}&group={groupId}&semestr=1&semest_part=1", nextTaskArgs);
            }
        }

        private IEnumerable<ParseTask> ScheduleTask(HtmlDocument document, Dictionary<string, string> taskArgs)
        {
            Logger.LogInformation($"Parse semester {taskArgs[ConstKeys.SemesterId]}, institute {taskArgs[ConstKeys.InstituteName]}, group {taskArgs[ConstKeys.GroupName]}");

            var rows = document.DocumentNode.SelectNodes("//div[@id='stud']/table/tr");
            var weekDay = string.Empty;
            var groupSubjects = new List<SubjectSharedModel>();
            
            var groupModel = new GroupSharedModel
            {
                GroupName = taskArgs[ConstKeys.GroupName],
                InstituteName = taskArgs[ConstKeys.InstituteName],
                SemesterId =  taskArgs[ConstKeys.SemesterId],
                SemesterPartId =  taskArgs[ConstKeys.SemesterPartId]
            };
            
            foreach (var row in rows)
            {
                var columns = row.SelectNodes("./td");
                if (columns == null || columns.Count < 1)
                {
                    continue;
                }

                if (columns.Count == 1)
                {
                    weekDay = columns.First().InnerText;
                    continue;
                }

                HtmlNode timeTable;
                string lessonNumber;
                if (columns.Count > 2)
                {
                    weekDay = columns.First().InnerText;
                    lessonNumber = columns[1].InnerText;
                    timeTable = columns.Last();
                }
                else
                {
                    lessonNumber = columns.First().InnerText;
                    timeTable = columns.Last();
                }
                
                var subjects = ParseTimeTable(timeTable, lessonNumber, weekDay);
                groupSubjects.AddRange(subjects);
            }

            groupModel.Subjects = groupSubjects;
            Task.Run(() => PostDataToServer(groupModel));
            yield break;
        }

        private IList<SubjectSharedModel> ParseTimeTable(HtmlNode html, string lessonNumber, string weekDay)
        {
            var result = new List<SubjectSharedModel>();
            
            var weekRows = html.SelectNodes("./table/tr");
            for (int i = 0; i < weekRows.Count; i++)
            {
                var weekRow = weekRows[i];
                var subgroups = weekRow.SelectNodes("./td");
                
                for (int j = 0; j < subgroups.Count; j++)
                {
                    int lesson = 0;
                    try
                    {
                        lesson = int.Parse(lessonNumber);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    var subjectModel = new SubjectSharedModel
                    {
                        LessonNumber = lesson,
                        WeekNumber = weekRows.Count > 1 ? i : 0,
                        SubgroupNumber = subgroups.Count > 1 ? j: 0,
                        DayOfWeek = WeekDays[weekDay]
                    };
                    
                    var subgroup = subgroups[j];
                    var subject = subgroup.SelectSingleNode("./div");
                    if (subject == null)
                    {
                        subjectModel.IsEmpty = true;
                        result.Add(subjectModel);
                        //For second week lesson is empty
                        continue;
                    }
                    
                    var subjectName = subject.SelectSingleNode("./b").InnerHtml;
                    var teacherName = subject.SelectSingleNode("./i").InnerText;
                    string roomName;
                    var rooms = subject.SelectNodes("./br/following-sibling::text()");
                    if (rooms.Count > 1)
                    {
                        roomName = WebUtility.HtmlDecode(rooms[1].InnerText);
                    }
                    else
                    {
                        roomName = WebUtility.HtmlDecode(rooms[0].InnerText);
                    }

                    roomName = roomName.Replace("\n", string.Empty);

                    subjectModel.SubjectName = subjectName;
                    subjectModel.TeacherName = teacherName;
                    subjectModel.BuildingName = GetBuildingName(roomName);
                    subjectModel.RoomNumber = GetRoomNumber(roomName);
                    subjectModel.SubjectType = SubjectTypes[GetSubjectType(roomName)];
                    
                    result.Add(subjectModel);
                    Logger.LogInformation($"Parsed subject: {subjectName} {teacherName} {roomName}");
                }
            }

            return result;
        }

        private async Task PostDataToServer(GroupSharedModel groupModel)
        {
            try
            {
                var httpClient = HttpClientFactory.Create();
                var response = await httpClient.PostAsJsonAsync(_apiUrl, groupModel);
                if (response.IsSuccessStatusCode)
                {
                    Logger.LogInformation($"Group {groupModel.InstituteName}-{groupModel.GroupName} successfully updated");
                }
                else
                {
                    Logger.LogInformation($"Group {groupModel.InstituteName}-{groupModel.GroupName} failed to update with message: {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception e)
            {
               Logger.LogError(e.Message, e);
            }

        }

        private string GetSubjectType(string roomName)
        {
            var parts = roomName.Split(",");
            return parts[1].Trim();
        }

        private string GetRoomNumber(string roomName)
        {
            var buildingPart = roomName.Split(",")[0].Split(".");
            return buildingPart[buildingPart.Length - 1].Trim();
        }

        private string GetBuildingName(string roomName)
        {
            var buildingPart = roomName.Split(",")[0];
            var dotIndex = buildingPart.LastIndexOf('.');
            var building = buildingPart.Substring(0, dotIndex+1);
            
            return building.Trim();
        }
    }
}