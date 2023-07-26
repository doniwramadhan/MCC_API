using APIMCC.Utilities.Enums;

namespace APIMCC.DTOs.Rooms
{
    public class BookedRoomDto
    {
        //Book data
        public Guid BookingGuid { get; set; }
        public string RoomName { get; set; }
        public StatusLevel Status { get; set; }
        //Room data
        public int Floor { get; set; }
        public string BookedBy { get; set; }
    }
}
