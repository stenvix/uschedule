using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Core.Enums;
using USchedule.Models.Domain;
using USchedule.Shared.Models;

namespace USchedule.Models.Profiles
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, LessonModel>()
                .ForMember(dest => dest.Teacher, src => src.MapFrom(i => i.TeacherSubject.Teacher))
                .ForMember(dest => dest.Subject, src => src.MapFrom(i => i.TeacherSubject.Subject));
            CreateMap<LessonModel, Lesson>()
                .ForMember(dest => dest.TeacherSubject, src => src.MapFrom(i =>
                    new TeacherSubject
                    {
                        SubjectId = i.Subject.Id,
                        TeacherId = i.Teacher.Id
                    }))
                .ForMember(dest => dest.TimeId, src => src.MapFrom(i => i.Time.Id))
                .ForMember(dest => dest.GroupId, src => src.MapFrom(i => i.Group.Id))
                .ForMember(dest => dest.RoomId, src => src.MapFrom(i => i.Room.Id))
                
                .ForMember(dest => dest.LessonTime, src => src.Ignore())
                .ForMember(dest => dest.Group, src => src.Ignore())
                .ForMember(dest => dest.Room, src => src.Ignore())
                .ForMember(dest => dest.TeacherSubjectId, src => src.Ignore());

            CreateMap<SubjectSharedModel, LessonModel>()
                .ForMember(dest => dest.Week, src => src.MapFrom(i => (WeekPart) i.WeekNumber))
                .ForMember(dest => dest.Subgroup, src => src.MapFrom(i => (SubgroupType) i.SubgroupNumber))
                .ForMember(dest => dest.Day, src => src.MapFrom(i => i.DayOfWeek));
        }
    }
}