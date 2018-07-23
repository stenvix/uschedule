using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Room: BaseEntity
    {
        public string Number { get; set; }
        
        public Guid BuildingId { get; set; }
        
        public Building Building { get; set; }
    }
}