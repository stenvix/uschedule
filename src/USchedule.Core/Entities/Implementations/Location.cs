using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Location: BaseEntity
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}