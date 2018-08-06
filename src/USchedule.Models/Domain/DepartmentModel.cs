using System;
using Newtonsoft.Json;
using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class DepartmentModel: ShortTitleModel
    {
        public InstituteModel Institute { get; set; }
        public RoomModel Room { get; set; }
    }
}