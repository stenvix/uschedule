using System.Collections.Generic;

namespace USchedule.Shared.Models
{
    public class DepartmentSharedModel
    {
        public string Name { get; set; }
        public IList<TeacherSharedModel> Teachers { get; set; }
    }
}