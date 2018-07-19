using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Location: EntityBase
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        
        public Guid UniversityId { get; set; }
        
        public University University { get; set; }
    }
}