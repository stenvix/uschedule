using USchedule.Models.Domain.Base;

namespace USchedule.Models.Domain
{
    public class SubjectModel: TitleModel
    {
        public UniversityModel University { get; set; }
    }
}