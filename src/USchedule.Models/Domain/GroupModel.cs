using USchedule.Core.Enums;
using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class GroupModel: TitleModel
    {
        public int EntryYear { get; set; }
        public DegreeType Degree { get; set; }
        public StudyForm StudyForm { get; set; }
        
        public DepartmentModel Department { get; set; }
    }
}