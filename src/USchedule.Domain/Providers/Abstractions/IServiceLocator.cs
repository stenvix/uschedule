using USchedule.Models.DTO;

namespace USchedule.Domain.Providers.Abstractions
{
    public interface IServiceLocator
    {
        T GetInstance<T>(params LocatorParameter[] parameters);
    }
}