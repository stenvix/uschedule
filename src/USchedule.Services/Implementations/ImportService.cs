using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using USchedule.Core.Enums;
using USchedule.Core.Helpers;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;
using USchedule.Models.DTO;
using USchedule.Services.Base;
using USchedule.Services.Responses.Base;
using USchedule.Shared.Models;

namespace USchedule.Services
{
    public class ImportService : BaseService, IImportService
    {
        private readonly IMapper _mapper;
        private string _universityName = "НУ \"ЛП\"";

        public ImportService(IManagerStore managerStore, IMapper mapper, ILogger<BaseService> logger) : base(managerStore, logger)
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
                        var institute = await ManagerStore.InstituteManager.GetSystemAsync(university.Id);
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
                        
                        await ManagerStore.TeacherManager.CreateOrUpdateSubjects(teacherModel.Id, subjectsModels.Select(i=>i.Id).Distinct());
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
            
            var university = await ManagerStore.UniversityManager.GetByShortTitleAsync(_universityName);
            var institutesTitles = institutes.Where(i=> !string.IsNullOrEmpty(i.ShortTitle)).Select(i => i.ShortTitle);
            var institutesModels = await ManagerStore.InstituteManager.GetAllByShortTitleAsync(institutesTitles, university.Id);
            var departmentsModels = await ManagerStore.DepartmentManager.GetAllSystemAsync(institutesModels.Select(i=>i.Id));
            var lessonTimesModels = await ManagerStore.LessonTimeManager.GetByUniversityAsync(university.Id);

            foreach (var institute in institutes.Where(i => !string.IsNullOrEmpty(i.ShortTitle)))
            {
                var instituteModel = institutesModels.FirstOrDefault(i => i.ShortTitle == institute.ShortTitle);
                if (instituteModel == null)
                {
                    continue;
                }
                var departmentModel = departmentsModels.First(i => i.InstituteId == instituteModel.Id);

                try
                {
                    foreach (var group in institute.Groups)
                    {
                        var usefullSubjects = group.Subjects.Where(i => !i.IsEmpty).ToList();
                        var subjectsTitles = usefullSubjects.Where(i=>!i.IsEmpty).Select(i => i.SubjectName).Distinct().ToList();
                        var subjectsModels = await ManagerStore.SubjectManager.GetAllByTitleAsync(subjectsTitles, university.Id);
                        var teacherSubjects = usefullSubjects.Select(i =>
                        {
                            var subject = subjectsModels.FirstOrDefault(s => s.Title == i.SubjectName);
                            if (subject == null)
                            {
                                var subjectModel = new SubjectModel
                                {
                                    Title = i.SubjectName,
                                    University = university
                                };
                                subject =  ManagerStore.SubjectManager.CreateAsync(subjectModel).GetAwaiter().GetResult();
                                subjectsModels.Add(subject);
                            }
                            
                            return new SearchTeacherSubject
                            {
                                SubjectId = subject.Id,
                                FirstName = i.TeacherFirstName,
                                LastName = i.TeacherLastName
                            };
                            
                        }).ToList();
                        var subjectsTeachersModels = await ManagerStore.TeacherManager.GetAllBySubjectsAsync(teacherSubjects, university.Id);
                        var groupModel = await GetOrCreateGroup(group, departmentModel);
                        var buildinsRooms = usefullSubjects.GroupBy(i => i.BuildingName).ToDictionary(i => i.Key, i => i.Select(r => string.IsNullOrEmpty(r.RoomNumber) ? r.BuildingName: r.RoomNumber).ToList());
                        var roomsModels = await GetOrUpsertRooms(buildinsRooms, university.Id);
                        
                        var lessonsList = new List<LessonModel>();
                        
                        foreach (var subject in usefullSubjects)
                        {
                            RoomModel roomModel;
                            LessonTimeModel time = lessonTimesModels.First(i => i.Number == subject.LessonNumber);
                            SubjectModel subjectModel = subjectsModels.First(i => i.Title == subject.SubjectName);

                            TeacherModel teacherModel;
                            if (!subjectsTeachersModels.ContainsKey(subjectModel.Id))
                            {
                                teacherModel = await CreateTeacher(subject, departmentModel, subjectModel.Id);
                                subjectsTeachersModels.Add(subjectModel.Id, new List<TeacherModel>{teacherModel});
                            }
                            else
                            {
                                teacherModel = subjectsTeachersModels[subjectModel.Id].FirstOrDefault(i =>
                                    i.LastName == subject.TeacherLastName &&
                                    i.FirstName.StartsWith(subject.TeacherFirstName));

                                if (teacherModel == null)
                                {
                                    teacherModel = await CreateTeacher(subject, departmentModel, subjectModel.Id);
                                    subjectsTeachersModels[subjectModel.Id].Add(teacherModel);
                                }
                            }

                            if (string.IsNullOrEmpty(subject.RoomNumber))
                            {
                                var buildingRoom = roomsModels.First(i=>i.Key.ShortTitle == AppConstants.SystemEntity);
                                roomModel = buildingRoom.Value.First(i => i.Number == subject.BuildingName);
                            }
                            else
                            {
                                try
                                {
                                    var test = roomsModels.First(i => i.Key.ShortTitle == subject.BuildingName);
                                }
                                catch (Exception e)
                                {
                                    Logger.LogError(e.Message, e);
                                    
                                }
                                
                                var buildingRoom = roomsModels.First(i => i.Key.ShortTitle == subject.BuildingName);
                                roomModel = buildingRoom.Value.First(i => i.Number == subject.RoomNumber);
                            }
                            
                            var lessonModel = _mapper.Map<LessonModel>(subject);
                            lessonModel.Group = groupModel;
                            lessonModel.Teacher = teacherModel;
                            lessonModel.Subject = subjectModel;
                            lessonModel.Room = roomModel;
                            lessonModel.Time = time;
                            lessonsList.Add(lessonModel);
                        }
                        
                        await ManagerStore.LessonManager.UpsertRangeAsync(lessonsList, groupModel.Id);

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

        private async Task<TeacherModel> CreateTeacher(SubjectSharedModel subject, DepartmentModel department, Guid subjectId)
        {
            var teacher = new TeacherModel
            {
                FirstName = subject.TeacherFirstName,
                LastName = subject.TeacherLastName,
                Department = department
            };

            teacher = await ManagerStore.TeacherManager.CreateAsync(teacher);
            await ManagerStore.TeacherManager.CreateOrUpdateSubjects(teacher.Id, new[] {subjectId});

            return teacher;
        }

        private async Task<Dictionary<BuildingModel, List<RoomModel>>> GetOrUpsertRooms(Dictionary<string, List<string>> buildingRooms, Guid universityId)
        {
            var buildingsModels = await ManagerStore.BuildingManager.GetAllByShortTitleAsync(buildingRooms.Select(i=>i.Key).ToList(), universityId);
            var systemBuildingModel = await ManagerStore.BuildingManager.GetSystemAsync(universityId);
            
            var buildingsRoomsModels = new Dictionary<BuildingModel, List<RoomModel>>();
            
            foreach (var buildingRoom in buildingRooms)
            {
                var buildingModel = buildingsModels.FirstOrDefault(i => i.ShortTitle == buildingRoom.Key);
                if (buildingModel == null)
                {
                    buildingModel = systemBuildingModel;
                }

                var existedRooms = await ManagerStore.RoomManager.GetAllByNumber(buildingRoom.Value, buildingModel.Id);
                var roomsNumbersToCreate = buildingRoom.Value.Except(existedRooms.Select(i => i.Number));
                var roomsToCreate = new List<RoomModel>();
                
                foreach (var roomNumber in roomsNumbersToCreate)
                {
                    var room = new RoomModel
                    {
                        Building = buildingModel,
                        Number = roomNumber
                    };
                    roomsToCreate.Add(room);
                }

                var createdRooms = await ManagerStore.RoomManager.CreateRangeAsync(roomsToCreate);
                var allRooms = new List<RoomModel>();
                allRooms.AddRange(existedRooms);
                allRooms.AddRange(createdRooms);
                if (buildingsRoomsModels.ContainsKey(buildingModel))
                {
                    buildingsRoomsModels[buildingModel].AddRange(allRooms);
                }
                else
                {
                    buildingsRoomsModels.Add(buildingModel, allRooms);                    
                }
            }

            return buildingsRoomsModels;
        }
    }
}