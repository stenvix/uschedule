using System;
using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class LessonTimeModel: BaseModel
    {
        public int Number { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        
        public UniversityModel University { get; set; }
    }
}