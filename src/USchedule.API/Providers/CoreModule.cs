using Autofac;
using USchedule.Domain.Managers.Base;

namespace USchedule.API.Providers
{
    public class CoreModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ServiceLocator>().AsImplementedInterfaces();
        }
    }
}