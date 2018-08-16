using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;
using USchedule.Shared.Models;

namespace USchedule.Models.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectModel>();
            CreateMap<SubjectModel, Subject>()
                .ForMember(dest => dest.UniversityId, src => src.MapFrom(i => i.University.Id))
                .ForMember(dest => dest.University, src => src.Ignore());
            CreateMap<TeacherSubjectSharedModel, SubjectModel>()
                .ForMember(dest => dest.Title, src => src.MapFrom(i => i.Title))
                .ForMember(dest => dest.University, src => src.Ignore())
                .ForMember(dest => dest.Id, src => src.Ignore());
        }
    }
}