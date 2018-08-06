using Autofac;
using USchedule.Services;

namespace USchedule.API.Providers
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UniversityService>().As<IUniversityService>();
            builder.RegisterType<InstituteService>().As<IInstituteService>();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>();
            builder.RegisterType<LessonService>().As<ILessonService>();
            builder.RegisterType<GroupService>().As<IGroupService>();
            builder.RegisterType<ImportService>().As<IImportService>();
        }
    }
}