using System;
using USchedule.Shared.Enums;

namespace USchedule.Shared.Models
{
    public class SubjectSharedModel
    {
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string RoomNumber { get; set; }
        public string BuildingName { get; set; }
        public SubjectTypeShared SubjectType { get; set; }
        public int LessonNumber { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int WeekNumber { get; set; }
        public int SubgroupNumber { get; set; }
        public bool IsEmpty { get; set; }
    }
}