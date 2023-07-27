using APIMCC.Utilities.Enums;

namespace APIMCC.DTOs.Bookings
{
    public class DetailBookingDto
    {
        //Employee
        public Guid BookingGuid { get; set; }
        public string BookedNik { get; set; }
        public string BookedBy { get; set; }
        
        //Room
        public string RoomName { get; set; }
       
        //Booking
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
    } 
}
