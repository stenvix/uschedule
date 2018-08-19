using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USchedule.Domain.Managers.Base;
using USchedule.Models.Domain;

namespace USchedule.Domain.Managers
{
    public interface IRoomManager: IManager<RoomModel>
    {
        Task<RoomModel> GetByNumber(string title, Guid buildingId);
        Task<IList<RoomModel>> GetAllByNumber(IList<string> titles, Guid buildingId);
        Task<IList<RoomModel>> CreateRangeAsync(IList<RoomModel> models);
    }
}