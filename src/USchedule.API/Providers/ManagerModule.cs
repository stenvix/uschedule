using Autofac;
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
            builder.RegisterType<ManagerStore>().As<IManagerStore>();
        }
    }
}