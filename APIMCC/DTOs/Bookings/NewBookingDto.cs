﻿using APIMCC.Models;
using APIMCC.Utilities.Enums;

namespace APIMCC.DTOs.Bookings
{
    public class NewBookingDto
    {
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }

        public static implicit operator Booking(NewBookingDto newBookingDto)
        {
            return new Booking
            {
                Guid = new Guid(),
                RoomGuid = newBookingDto.RoomGuid,
                EmployeeGuid = newBookingDto.EmployeeGuid,
                StartDate = newBookingDto.StartDate,
                EndDate = newBookingDto.EndDate,
                Status = newBookingDto.Status,
                Remarks = newBookingDto.Remarks,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
