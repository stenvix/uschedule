using Microsoft.Extensions.Logging;
using USchedule.Domain.Managers.Base;

namespace USchedule.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IManagerStore ManagerStore;
        protected readonly ILogger<BaseService> Logger;

        protected BaseService(IManagerStore managerStore, ILogger<BaseService> logger)
        {
            ManagerStore = managerStore;
            Logger = logger;
        }
    }
}