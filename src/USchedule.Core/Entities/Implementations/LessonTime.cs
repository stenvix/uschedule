using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class LessonTime : BaseEntity
    {
        public int Number { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public Guid UniversityId { get; set; }

        public University University { get; set; }
    }
}