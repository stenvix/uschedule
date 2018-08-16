using System.Collections.Generic;

namespace USchedule.Shared.Models
{
    public class TeacherSharedModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<TeacherSubjectSharedModel> Subjects { get; set; }
    }
}