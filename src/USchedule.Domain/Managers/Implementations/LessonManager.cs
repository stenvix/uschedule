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
    public class LessonManager: BaseManager<LessonModel, Lesson>, ILessonManager
    {
        public LessonManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<LessonModel, Lesson>> logger) : base(unitOfWork, unitOfWork.LessonRepository, mapper, logger)
        {
        }

        public async Task<IList<LessonModel>> GetByGroupAsync(Guid groupId)
        {
            var entities = await Repository.FindAllAsync(i => i.GroupId == groupId);
            return Mapper.Map<IList<LessonModel>>(entities);
        }
    }
}