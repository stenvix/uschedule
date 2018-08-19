using Autofac;
using USchedule.Core.Entities.Implementations;
using USchedule.Domain.Managers;
using USchedule.Domain.Managers.Base;

namespace USchedule.API.Providers
{
    public class ManagerModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<AppUnitOfWork>().AsImplementedInterfaces();
            builder.RegisterType<UniversityManager>().AsImplementedInterfaces();
            builder.RegisterType<InstituteManager>().AsImplementedInterfaces();
            builder.RegisterType<DepartmentManager>().AsImplementedInterfaces();
            builder.RegisterType<LessonManager>().AsImplementedInterfaces();
            builder.RegisterType<TeacherManager>().AsImplementedInterfaces();
            builder.RegisterType<SubjectManager>().AsImplementedInterfaces();
            builder.RegisterType<GroupManager>().AsImplementedInterfaces();
            builder.RegisterType<BuildingManager>().AsImplementedInterfaces();
            builder.RegisterType<RoomManager>().AsImplementedInterfaces();
            builder.RegisterType<LessonTimeManager>().AsImplementedInterfaces();
            builder.RegisterType<ManagerStore>().As<IManagerStore>();
        }
    }
}