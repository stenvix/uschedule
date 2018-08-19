using Microsoft.EntityFrameworkCore;
using USchedule.Core.Entities.Implementations;

namespace USchedule.Persistence.Repositories
{
    public class RoomRepository: Repository<Room>, IRoomRepository
    {
        public RoomRepository(DbContext context) : base(context)
        {
        }
    }
}