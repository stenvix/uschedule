using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Entities.Implementations;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public class TeacherManager : BaseManager<TeacherModel, Teacher>, ITeacherManager
    {
        public TeacherManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<TeacherModel,
            Teacher>> logger) : base(unitOfWork, unitOfWork.TeacherRepository, mapper, logger)
        {
        }

        public async Task<bool> Exists(string firstName, string lastName)
        {
            return await Repository.Exists(i => i.FirstName == firstName && i.LastName == lastName);
        }

        public async Task CreateRangeAsync(IList<TeacherModel> teachers)
        {
            var entities = Mapper.Map<IList<Teacher>>(teachers);
            var existed = await UnitOfWork.TeacherRepository.GetExistedAsync(entities);
            var entitiesToCreate = entities
                .Where(i => existed.All(t => t.FirstName == i.FirstName && t.LastName == i.LastName)).ToList();

            if (entitiesToCreate.Any())
            {
                await UnitOfWork.TeacherRepository.CreateRangeAsync(entitiesToCreate);
                await UnitOfWork.SaveChanges();
            }
        }
    }
}