using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Entities.Implementations;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public class GroupManager: BaseManager<GroupModel, Group>, IGroupManager
    {
        public GroupManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<GroupModel, Group>> logger) : base(unitOfWork, unitOfWork.GroupRepository, mapper, logger)
        {
        }

        public async Task<IList<GroupModel>> GetByInstituteAsync(Guid instituteId)
        {
            var entities = await Repository.FindAllAsync(i => i.Department.InstituteId == instituteId);
            return Mapper.Map<IList<GroupModel>>(entities);
        }
    }
}