using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Institute: ShortTitleEntityBase
    {
        public Guid BuildingId { get; set; }
        public Guid OrganisationId { get; set; }
        
        public Building Building { get; set; }
        public Organisation Organisation { get; set; }
    }
}