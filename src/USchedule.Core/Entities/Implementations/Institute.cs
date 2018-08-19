using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Institute: ShortTitleEntity
    {
        public Guid BuildingId { get; set; }
        public Guid UniversityId { get; set; }
        
        public Building Building { get; set; }
        public University University { get; set; }
        
        public bool IsSystem { get; set; }
    }
}