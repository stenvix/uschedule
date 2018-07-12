using System;
using USchedule.Core.Entities.Implementations.Base;
using USchedule.Core.Enums;

namespace USchedule.Core.Entities.Implementations
{
    public class TeacherSubject : EntityBase
    {
        public SubjectType Type { get; set; }

        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }

        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}