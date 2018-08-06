using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;
using USchedule.Shared.Models;

namespace USchedule.Models.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherModel>();
            CreateMap<TeacherModel, Teacher>()
                .ForMember(dest => dest.DepartmentId, src => src.MapFrom(i => i.Department.Id))
                .ForMember(dest => dest.Department, src => src.Ignore());

            CreateMap<TeacherSharedModel, TeacherModel>();
        }
    }
}