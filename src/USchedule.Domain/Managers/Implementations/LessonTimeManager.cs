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
    public class LessonTimeManager: BaseManager<LessonTimeModel, LessonTime>, ILessonTimeManager
    {
        public LessonTimeManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<LessonTimeModel, LessonTime>> logger) : base(unitOfWork, unitOfWork.LessonTimeRepository, mapper, logger)
        {
        }

        public async Task<LessonTimeModel> GetByNumberAsync(int lessonNumber)
        {
            var entity = await Repository.FindAsync(i => i.Number == lessonNumber);
            return Mapper.Map<LessonTimeModel>(entity);
        }

        public async Task<IList<LessonTimeModel>> GetAllByNumberAsync(IEnumerable<int> lessonNumbers, Guid universityId)
        {
            var entity = await Repository.FindAsync(i => i.UniversityId == universityId && lessonNumbers.Contains(i.Number));
            return Mapper.Map<IList<LessonTimeModel>>(entity);
        }

        public async Task<IList<LessonTimeModel>> GetByUniversityAsync(Guid universityId)
        {
            var entity = await Repository.FindAllAsync(i => i.UniversityId == universityId);
            return Mapper.Map<IList<LessonTimeModel>>(entity);
        }
    }
}