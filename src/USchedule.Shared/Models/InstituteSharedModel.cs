using System.Collections.Generic;

namespace USchedule.Shared.Models
{
    public class InstituteSharedModel
    {
        public string ShortTitle { get; set; }
        public IList<GroupSharedModel> Groups { get; set; }
        
        public InstituteSharedModel()
        {
            Groups = new List<GroupSharedModel>();
        }

    }
}