using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;
using USchedule.Shared.Models;

namespace USchedule.Models.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentModel>();
            CreateMap<DepartmentModel, Department>()
                .ForMember(dest => dest.InstituteId, src => src.MapFrom(i => i.Institute.Id))
                .ForMember(dest => dest.Institute, src => src.Ignore());

            CreateMap<DepartmentSharedModel, DepartmentModel>()
                .ForMember(dest => dest.Title, src => src.MapFrom(i => i.Name));
        }
    }
}