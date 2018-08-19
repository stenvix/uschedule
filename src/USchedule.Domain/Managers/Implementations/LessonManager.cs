using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        public async Task UpsertRangeAsync(IList<LessonModel> models, Guid groupId)
        {
            var entities = Mapper.Map<IList<Lesson>>(models);
            var teacherSubjects = await UnitOfWork.TeacherSubjectRepository.GetIds(entities.Select(i => i.TeacherSubject));
            
            var existed = (await Repository.FindAllAsync(i => i.GroupId == groupId)).ToList();
            
            var entitiesToCreate = new List<Lesson>();
            var entitiesToUpdate = new List<Lesson>();
            var entitiesToDelete = new List<Guid>();

            foreach (var dayLessons in entities.GroupBy(i => i.Day))
            {
                var byTime = dayLessons.GroupBy(i => i.TimeId);
                foreach (var timeLessons in byTime)
                {
                    var existedLessons = existed.Where(i=>i.Day == dayLessons.Key && i.TimeId == timeLessons.Key).ToList();
                    foreach (var lesson in timeLessons)
                    {
                        var teacherSubject = teacherSubjects.First(i =>
                            i.SubjectId == lesson.TeacherSubject.SubjectId &&
                            i.TeacherId == lesson.TeacherSubject.TeacherId);
                        lesson.TeacherSubjectId = teacherSubject.Id;

                        
                        var existedLesson = existedLessons.FirstOrDefault(i=>i.Week == lesson.Week && i.Subgroup == lesson.Subgroup);
                        if (existedLesson != null)
                        {
                            existedLessons.Remove(existedLesson);   
                            Mapper.Map(lesson, existedLesson);
                            entitiesToUpdate.Add(existedLesson);
                        }
                        else
                        {
                            entitiesToCreate.Add(lesson);
                        }
                    }
                    entitiesToDelete.AddRange(existedLessons.Select(i=>i.Id));
                }
            }

            

            if (entitiesToDelete.Any())
            {
                foreach (var id in entitiesToDelete)
                {
                    await Repository.DeleteAsync(id);
                }
                await UnitOfWork.SaveChanges();
            }
            
            if (entitiesToUpdate.Any())
            {
                foreach (var lesson in entitiesToUpdate)
                {
                    await Repository.Update(lesson);

                }
                await UnitOfWork.SaveChanges();
            }

            if (entitiesToCreate.Any())
            {
                await Repository.CreateRangeAsync(entitiesToCreate);
                await UnitOfWork.SaveChanges();
            }
        }
    }
}