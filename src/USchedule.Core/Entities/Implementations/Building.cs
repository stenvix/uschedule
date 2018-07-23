﻿using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Building: ShortTitleEntityBase
    {
        public Guid LocationId { get; set; }
        public Guid UniversityId { get; set; }
        
        public Location Location { get; set; }
        public University University { get; set; }
    }
}