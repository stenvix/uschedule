﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface IGroupManager: IManager<GroupModel>
    {
        Task<IList<GroupModel>> GetByInstituteAsync(Guid instituteId);
        Task<GroupModel> GetByTitleAsync(string title);
    }
}