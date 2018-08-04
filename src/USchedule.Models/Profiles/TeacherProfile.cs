using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class TeacherProfile: Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherModel>();
            CreateMap<TeacherModel, Teacher>();
        }
    }
}