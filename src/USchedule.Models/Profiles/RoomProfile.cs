using AutoMapper;
using USchedule.Core.Entities.Implementations;
using USchedule.Models.Domain;

namespace USchedule.Models.Profiles
{
    public class RoomProfile: Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomModel>();
            CreateMap<RoomModel, Room>();
        }
    }
}