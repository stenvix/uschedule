using System;
using USchedule.Core.Entities.Implementations.Base;
using USchedule.Core.Enums;

namespace USchedule.Core.Entities.Implementations
{
    public class Lesson: EntityBase
    {
        public SubjectType Type { get; set; }

        public Guid GroupId { get; set; }
        public Guid TeacherSubjectId { get; set; }
        public Guid RoomId { get; set; }
        public Guid TimeId { get; set; }
        public Guid SemesterId { get; set; }

        
        public Group Group { get; set; }
        public TeacherSubject TeacherSubject { get; set; }
        public Room Room { get; set; }
        public LessonTime LessonTime { get; set; }
        public Semester Semester { get; set; }
    }
}