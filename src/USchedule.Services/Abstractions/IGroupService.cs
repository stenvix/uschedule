﻿using System;
using System.Threading.Tasks;
using USchedule.Models.Domain;
using USchedule.Services.Responses.Base;

namespace USchedule.Services
{
    public interface IGroupService
    {
        Task<ItemsResponse<GroupModel>> GetByInstitute(Guid instituteId);
    }
}