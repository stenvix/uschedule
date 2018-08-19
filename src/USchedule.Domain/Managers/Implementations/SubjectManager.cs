using System;
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
    public class SubjectManager : BaseManager<SubjectModel, Subject>, ISubjectManager
    {
        public SubjectManager(IAppUnitOfWork unitOfWork, IMapper mapper,
            ILogger<BaseManager<SubjectModel, Subject>> logger) : base(unitOfWork, unitOfWork.SubjectRepository, mapper,
            logger)
        {
        }

        public async Task<IList<SubjectModel>> CreateRangeAsync(Guid universityId, IList<SubjectModel> models)
        {
            var entities = Mapper.Map<IList<Subject>>(models);
            var existed = await UnitOfWork.SubjectRepository.GetExistedAsync(entities);
            var entitiesToCreate = entities.Where(i => existed.All(s => s.Title != i.Title)).ToList();
            if (entitiesToCreate.Any())
            {
                await UnitOfWork.SubjectRepository.CreateRangeAsync(entitiesToCreate);
                await UnitOfWork.SaveChanges();
            }

            existed.AddRange(entitiesToCreate);
            return Mapper.Map<IList<SubjectModel>>(existed);
        }

        public async Task<SubjectModel> GetByTitleAsync(string title)
        {
            var entity = await Repository.FindAsync(i => i.Title == title);
            return Mapper.Map<SubjectModel>(entity);
        }

        public async Task<IList<SubjectModel>> GetAllByTitleAsync(IEnumerable<string> subjectsTitles, Guid universityId)
        {
            var entities = await Repository.FindAllAsync(i => i.UniversityId == universityId && subjectsTitles.Contains(i.Title));
            return Mapper.Map<IList<SubjectModel>>(entities);
        }
    }
}