﻿using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Subject: TitleEntity
    {
        public Guid UniversityId { get; set; }
        
        public University University { get; set; }
    }
}