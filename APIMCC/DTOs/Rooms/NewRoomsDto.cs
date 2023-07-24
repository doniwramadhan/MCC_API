using APIMCC.Models;

namespace APIMCC.DTOs.Rooms
{
    public class NewRoomsDto
    {
        public String Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }

        public static implicit operator Room(NewRoomsDto newRoomsDto)
        {
            return new Room
            {
                Guid = new Guid(),
                Name = newRoomsDto.Name,
                Floor = newRoomsDto.Floor,
                Capacity = newRoomsDto.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
