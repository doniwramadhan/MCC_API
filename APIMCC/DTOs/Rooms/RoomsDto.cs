using APIMCC.DTOs.Universities;
using APIMCC.Models;

namespace APIMCC.DTOs.Rooms
{
    public class RoomsDto
    {
        public Guid Guid { get; set; }
        public String Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }

        public static implicit operator Room(RoomsDto roomsDto)
        {
            return new Room
            {
                Guid = roomsDto.Guid,
                Name = roomsDto.Name,
                Floor = roomsDto.Floor,
                Capacity = roomsDto.Capacity,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator RoomsDto(Room room)
        {
            return new RoomsDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity,
            };
        }
    }
}
