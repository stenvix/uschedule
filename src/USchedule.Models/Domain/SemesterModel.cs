using System;
using USchedule.Core.Enums;

namespace USchedule.Models.Domain
{
    public class SemesterModel
    {
        public SemesterType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public UniversityModel University { get; set; }
    }
}