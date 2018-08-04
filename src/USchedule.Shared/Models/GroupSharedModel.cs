using System.Collections.Generic;

namespace USchedule.Shared.Models
{
    public class GroupSharedModel
    {
        public string InstituteName { get; set; }
        public string GroupName { get; set; }
        public string SemesterId { get; set; }
        public string SemesterPartId { get; set; }
        
        public IList<SubjectSharedModel> Subjects { get; set; }

        public GroupSharedModel()
        {
            Subjects = new List<SubjectSharedModel>();
        }
    }
}