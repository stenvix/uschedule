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
    public class DepartmentManager: BaseManager<DepartmentModel, Department>, IDepartmentManager
    {
        public DepartmentManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<DepartmentModel, Department>> logger) : base(unitOfWork, unitOfWork.DepartmentRepository, mapper, logger)
        {
        }

        public async Task<IList<DepartmentModel>> GetByInstituteAsync(Guid instituteId)
        {
            var entities = await Repository.FindAllAsync(i => i.InstituteId == instituteId);
            return Mapper.Map<IList<DepartmentModel>>(entities);
        }

        public async Task<DepartmentModel> GetByTitleAsync(string departmentTitle)
        {
            var entity = await Repository.FindAsync(i => i.Title == departmentTitle);
            return Mapper.Map<DepartmentModel>(entity);
        }
    }
}