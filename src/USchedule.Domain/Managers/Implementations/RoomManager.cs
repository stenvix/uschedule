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
    public class RoomManager : BaseManager<RoomModel, Room>, IRoomManager
    {
        public RoomManager(IAppUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseManager<RoomModel, Room>> logger) :
            base(unitOfWork, unitOfWork.RoomRepository, mapper, logger)
        {
        }

        public async Task<RoomModel> GetByNumber(string title, Guid buildingId)
        {
            var entity = await Repository.FindAsync(i => i.Number == title && i.BuildingId == buildingId);
            return Mapper.Map<RoomModel>(entity);
        }

        public async Task<IList<RoomModel>> GetAllByNumber(IList<string> titles, Guid buildingId)
        {
            var entities = await Repository.FindAllAsync(i => i.BuildingId == buildingId && titles.Contains(i.Number));
            return Mapper.Map<IList<RoomModel>>(entities);
        }

        public async Task<IList<RoomModel>> CreateRangeAsync(IList<RoomModel> models)
        {
            var entities = Mapper.Map<IList<Room>>(models);
            
            await Repository.CreateRangeAsync(entities);
            await UnitOfWork.SaveChanges();

            return Mapper.Map<IList<RoomModel>>(entities);
        }
    }
}