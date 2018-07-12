using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Lesson: EntityBase
    {
        public Guid GroupId { get; set; }
        public Guid TeacherSubjectId { get; set; }
        public Guid RoomId { get; set; }
        public Guid TimeId { get; set; }
        
        public Group Group { get; set; }
        public TeacherSubject TeacherSubject { get; set; }
        public Room Room { get; set; }
        public Time Time { get; set; }
    }
}