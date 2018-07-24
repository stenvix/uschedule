using Autofac;
using USchedule.Services.Abstractions;
using USchedule.Services.Implementations;

namespace USchedule.API.Providers
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UniversityService>().As<IUniversityService>();
        }
    }
}