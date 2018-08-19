using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class GroupProfile: Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupModel>();
            CreateMap<GroupModel, Group>()
                .ForMember(dest=>dest.DepartmentId, src=>src.MapFrom(i=>i.Department.Id))
                .ForMember(dest=>dest.Department, src=>src.Ignore());
        }
    }
}