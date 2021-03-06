﻿using System;
using System.Collections.Generic;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class University: ShortTitleEntity
    {
        public string Site { get; set; }
        
        public ICollection<Building> Buildings { get; set; }
    }
}