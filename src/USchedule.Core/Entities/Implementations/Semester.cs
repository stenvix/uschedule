using System;
using USchedule.Core.Entities.Implementations.Base;
using USchedule.Core.Enums;

namespace USchedule.Core.Entities.Implementations
{
    public class Semester: BaseEntity
    {
        public SemesterType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public Guid UniversityId { get; set; }
        
        public University University { get; set; }
    }
}