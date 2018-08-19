using System;
using USchedule.Core.Enums;
using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class LessonModel : BaseModel
    {
        public SubjectType Type { get; set; }
        public WeekPart Week { get; set; }
        public SubgroupType Subgroup { get; set; }
        public DayOfWeek Day { get; set; }

        public GroupModel Group { get; set; }
        public TeacherModel Teacher { get; set; }
        public SubjectModel Subject { get; set; }
        public RoomModel Room { get; set; }
        public LessonTimeModel Time { get; set; }
        public SemesterModel Semester { get; set; }
    }
}