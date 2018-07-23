using System;
using USchedule.Core.Entities.Implementations.Base;
using USchedule.Core.Enums;

namespace USchedule.Core.Entities.Implementations
{
    public class Group: TitleEntity
    {
        public int EntryYear { get; set; }
        public DegreeType Degree { get; set; }
        public StudyForm StudyForm { get; set; }
        
        public Guid DepartmentId { get; set; }
        
        public Department Department { get; set; }
    }
}