using System.Linq;
using Autofac;
using Autofac.Core;
using USchedule.Domain.Providers.Abstractions;
using USchedule.Models.DTO;

namespace USchedule.API.Providers
{
    public class ServiceLocator: IServiceLocator
    {
        private readonly IComponentContext _container;

        public ServiceLocator(IComponentContext container)
        {
            _container = container;
        }
        
        public T GetInstance<T>(params LocatorParameter[] parameters)
        {
            return _container.Resolve<T>(parameters.Select(i => i.GetParameter()));
        }
    }
    
    public static class ParameterMapper
    {
        public static Parameter GetParameter(this LocatorParameter parameter)
        {
            if (!string.IsNullOrEmpty(parameter.Name))
            {
                return new NamedParameter(parameter.Name, parameter.Value);
            }

            return new TypedParameter(parameter.Type, parameter.Value);
        }
    }
}