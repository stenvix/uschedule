using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Entities.Implementations;
using USchedule.Core.Helpers;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public class DepartmentManager : BaseManager<DepartmentModel, Department>, IDepartmentManager
    {
        public DepartmentManager(IAppUnitOfWork unitOfWork, IMapper mapper,
            ILogger<BaseManager<DepartmentModel, Department>> logger) : base(unitOfWork,
            unitOfWork.DepartmentRepository, mapper, logger)
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

        public async Task<DepartmentModel> GetByShortTitleAsync(string shortTitle, Guid instituteId)
        {
            var entity = await Repository.FindAsync(i => i.ShortTitle == shortTitle && i.InstituteId == instituteId);
            return Mapper.Map<DepartmentModel>(entity);
        }

        public async Task<DepartmentModel> GetSystemAsync(Guid instituteId)
        {
            var entity = await Repository.FindAsync(i => i.InstituteId == instituteId && i.IsSystem);
            if (entity == null)
            {
                entity = new Department
                {
                    Title = AppConstants.SystemEntity,
                    ShortTitle = AppConstants.SystemEntity,
                    InstituteId = instituteId,
                    IsSystem = true,
                };
                entity = await Repository.CreateAsync(entity);
                await UnitOfWork.SaveChanges();
            }

            return Mapper.Map<DepartmentModel>(entity);
        }

        public async Task<IList<DepartmentModel>> GetAllSystemAsync(IEnumerable<Guid> institutesIds)
        {
            var existed = await Repository.FindAllAsync(i => institutesIds.Contains(i.InstituteId) && i.IsSystem);
            var idsToCreate = institutesIds.Except(existed.Select(i => i.InstituteId)).ToList();
            var entitiesToCreate = new List<Department>();
            if (idsToCreate.Any())
            {
                foreach (var instituteId in idsToCreate)
                {
                    var entity = new Department
                    {
                        Title = AppConstants.SystemEntity,
                        ShortTitle = AppConstants.SystemEntity,
                        InstituteId = instituteId,
                        IsSystem = true,
                    };
                    entitiesToCreate.Add(entity);
                }
                await Repository.CreateRangeAsync(entitiesToCreate);
                await UnitOfWork.SaveChanges();
            }
            entitiesToCreate.AddRange(existed);
            return Mapper.Map<IList<DepartmentModel>>(entitiesToCreate);
        }
    }
}