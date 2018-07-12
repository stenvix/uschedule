using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Group: TitleEntityBase
    {
        public int EntryYear { get; set; }
        
        public Guid InstituteId { get; set; }
        public Guid? DepartmentId { get; set; }
        
        public Institute Institute { get; set; }
        public Department Department { get; set; }
    }
}