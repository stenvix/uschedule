using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Time : EntityBase
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public Guid InstituteId { get; set; }

        public Institute Institute { get; set; }
    }
}