using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private string _nulpEmptyInstitute = "Відсутній";

        public ImportService(IManagerStore managerStore, IMapper mapper, ILogger<BaseService> logger) : base(
            managerStore, logger)
        {
            _mapper = mapper;
        }

        public async Task<BaseResponse> ImportDepartment(IList<DepartmentSharedModel> departments)
        {
            var response = new BaseResponse();

            foreach (var department in departments)
            {
                try
                {
                    var departmentModel = await ManagerStore.DepartmentManager.GetByTitleAsync(department.Name);
                    if (departmentModel == null)
                    {
                        var institute = await ManagerStore.InstituteManager.GetByShortTitleAsync(_nulpEmptyInstitute);
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

                    await ManagerStore.TeacherManager.CreateRangeAsync(teacherList.ToList());
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
    }
}