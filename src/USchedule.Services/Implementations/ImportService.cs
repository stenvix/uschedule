using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Enums;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;
using USchedule.Services.Base;
using USchedule.Services.Responses.Base;
using USchedule.Shared.Models;

namespace USchedule.Services
{
    public class ImportService : BaseService, IImportService
    {
        private readonly IMapper _mapper;
        private string _nulpEmpty = "Відсутній";
        private string _universityName = "НУ \"ЛП\"";

        public ImportService(IManagerStore managerStore, IMapper mapper, ILogger<BaseService> logger) : base(
            managerStore, logger)
        {
            _mapper = mapper;
        }

        public async Task<BaseResponse> ImportDepartments(IList<DepartmentSharedModel> departments)
        {
            var response = new BaseResponse();

            var university = await ManagerStore.UniversityManager.GetByShortTitleAsync(_universityName);

            foreach (var department in departments)
            {
                try
                {
                    var departmentModel = await ManagerStore.DepartmentManager.GetByTitleAsync(department.Name);
                    if (departmentModel == null)
                    {
                        var institute = await ManagerStore.InstituteManager.GetByShortTitleAsync(_nulpEmpty);
                        var model = _mapper.Map<DepartmentModel>(department);
                        model.Institute = institute;
                        departmentModel = await ManagerStore.DepartmentManager.CreateAsync(model);
                    }

                    var teacherList = department.Teachers.Select(i =>
                    {
                        var model = _mapper.Map<TeacherModel>(i);
                        model.Department = departmentModel;
                        return model;
                    });

                    var subjectList = department.Teachers.SelectMany(i => i.Subjects).Select(i =>
                    {
                        var model = _mapper.Map<SubjectModel>(i);
                        model.University = university;
                        return model;
                    }).GroupBy(i=>i.Title).Select(i=>i.First());

                    var teachers = await ManagerStore.TeacherManager.CreateRangeAsync(teacherList.ToList());
                    var subjects = await ManagerStore.SubjectManager.CreateRangeAsync(university.Id, subjectList.ToList());
                    foreach (var teacher in department.Teachers)
                    {
                        var teacherModel = teachers.First(i=> i.FirstName == teacher.FirstName && i.LastName == teacher.LastName);
                        var subjectsModels = subjects.Where(i => teacher.Subjects.Any(s => s.Title == i.Title)).ToList();
                        
                        await ManagerStore.TeacherManager.CreateOrUpdateSubjects(teacherModel.Id, subjectsModels.Select(i=>i.Id));
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError(e, e.Message);
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }

            return response;
        }

        public async Task<BaseResponse> ImportInstitutes(IList<InstituteSharedModel> institutes)
        {
            var response = new BaseResponse();

            foreach (var institute in institutes)
            {
                try
                {
                    var instituteModel = await ManagerStore.InstituteManager.GetByShortTitleAsync(institute.ShortTitle);
                    if (instituteModel == null)
                    {
                        continue;
                    }

                    var department = await GetOrCreateDepartment(instituteModel);

                    foreach (var group in institute.Groups)
                    {
                        var groupModel = await GetOrCreateGroup(group, department);
                        foreach (var subject in group.Subjects)
                        {
//                            subject.
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError(e, e.Message);
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }

            return response;
        }


        private async Task<GroupModel> GetOrCreateGroup(GroupSharedModel group, DepartmentModel department)
        {
            var groupModel = await ManagerStore.GroupManager.GetByTitleAsync(group.GroupName);
            if (groupModel != null)
            {
                return groupModel;
            }
            
            groupModel = new GroupModel
            {
                Title = group.GroupName,
                Degree = DegreeType.Bachelor,
                Department = department,
                EntryYear = DateTime.MinValue.Year,
                StudyForm = StudyForm.Day,
            };
            return await ManagerStore.GroupManager.CreateAsync(groupModel);
        }

        private async Task<DepartmentModel> GetOrCreateDepartment(InstituteModel institute)
        {
            var departmentModel = await ManagerStore.DepartmentManager.GetByShortTitleAsync(_nulpEmpty, institute.Id);
            if (departmentModel != null)
            {
                return departmentModel;
            }

            var emptyDepartment = new DepartmentModel
            {
                Institute = institute,
                ShortTitle = _nulpEmpty
            };

            return await ManagerStore.DepartmentManager.CreateAsync(emptyDepartment);
        }
        
//        private void CreateOrUpdate
    }
}