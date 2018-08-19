using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Entities.Implementations;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;
using USchedule.Models.DTO;

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

        public async Task<IList<TeacherModel>> CreateRangeAsync(IList<TeacherModel> teachers)
        {
            var entities = Mapper.Map<IList<Teacher>>(teachers);
            var existed = await UnitOfWork.TeacherRepository.GetExistedAsync(entities);
            var entitiesToCreate = entities
                .Where(i => !existed.Any(t => t.FirstName == i.FirstName && t.LastName == i.LastName)).ToList();

            if (entitiesToCreate.Any())
            {
                await UnitOfWork.TeacherRepository.CreateRangeAsync(entitiesToCreate);
                await UnitOfWork.SaveChanges();
            }

            existed.AddRange(entitiesToCreate);
            return Mapper.Map<IList<TeacherModel>>(existed);
        }

        public async Task CreateOrUpdateSubjects(Guid teacherId, IEnumerable<Guid> subjectsIds)
        {
            var existed = await UnitOfWork.TeacherSubjectRepository.GetByTeacherAsync(teacherId);
            var teacherSubjects = subjectsIds.Where(i => existed.All(s => s.SubjectId != i))
                .Select(i => new TeacherSubject {SubjectId = i, TeacherId = teacherId});
            await UnitOfWork.TeacherSubjectRepository.CreateRangeAsync(teacherSubjects);
            await UnitOfWork.SaveChanges();
        }

        public async Task<TeacherModel> GetBySubjectAsync(Guid subjectId, string lastName, string firstName)
        {
            var entity = await UnitOfWork.TeacherSubjectRepository.GetBySubjectAsync(subjectId, lastName, firstName);
            return await GetAsync(entity.TeacherId);
        }

        public async Task<Dictionary<Guid, IList<TeacherModel>>> GetAllBySubjectsAsync(
            IList<SearchTeacherSubject> teacherSubjects, Guid universityId)
        {
            var entities =
                await UnitOfWork.TeacherSubjectRepository.GetBySubjectsAsync(teacherSubjects.Select(i => i.SubjectId),
                    universityId);

            var teacherList = new Dictionary<Guid, IList<TeacherModel>>();

            foreach (var teacherSubject in teacherSubjects)
            {
                var subjectId = teacherSubject.SubjectId;

                var teacher = entities.FirstOrDefault(i =>
                    i.SubjectId == subjectId && i.Teacher.LastName == teacherSubject.LastName &&
                    i.Teacher.FirstName.StartsWith(teacherSubject.FirstName));

                if (teacher == null)
                {
                    continue;
                }

                var teacherModel = Mapper.Map<TeacherModel>(teacher.Teacher);

                if (teacherList.ContainsKey(subjectId))
                {
                    teacherList[subjectId].Add(teacherModel);
                }
                else
                {
                    var teachers = new List<TeacherModel>
                    {
                        teacherModel
                    };
                    teacherList.Add(subjectId, teachers);
                }
            }

            return teacherList;
        }
    }
}