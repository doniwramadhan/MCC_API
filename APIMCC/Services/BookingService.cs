using APIMCC.Contracts;
using APIMCC.DTOs.Bookings;
using APIMCC.DTOs.Educations;
using APIMCC.DTOs.Rooms;
using APIMCC.Models;

namespace APIMCC.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;
        public BookingService(IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
        }

        public IEnumerable<BookingLengthDto> GetBookingLength()
        {
            List<BookingLengthDto> listBookingLength = new List<BookingLengthDto>();
            var timeSpan = new TimeSpan();
            var bookings = GetAll();
            foreach (var booking in bookings)
            {
                var currentDate = booking.StartDate;
                var endDate = booking.EndDate;

                while (currentDate.Date <= endDate.Date)
                {
                    // Memeriksa apakah hari saat ini adalah Sabtu atau Minggu
                    if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        if (currentDate.Date == endDate.Date)
                        {
                            // Hari terakhir, menghitung sisa jam penyewaan
                            TimeSpan lastDay = currentDate - endDate;
                            timeSpan += lastDay;
                        }
                        else
                        {
                            // Hari kerja, menghitung waktu kerja dengan memperhitungkan jam
                            DateTime openRoom = currentDate.Date.AddHours(9); // Misalnya, waktu kerja dimulai pada pukul 09:00
                            DateTime closeRoom = currentDate.Date.AddHours(17).AddMinutes(30); // Misalnya, waktu kerja selesai pada pukul 17:30

                            TimeSpan dayTime = closeRoom - openRoom;
                            timeSpan += dayTime;
                        }
                    }
                    currentDate = currentDate.AddDays(1); // Pindah ke hari berikutnya
                }

                var room = _roomRepository.GetByGuid(booking.RoomGuid);

                var bookingLengthDto = new BookingLengthDto()
                {
                    RoomGuid = booking.RoomGuid,
                    RoomName = room.Name,
                    BookingLength = timeSpan.TotalHours
                };
                listBookingLength.Add(bookingLengthDto);
            }

            if (!listBookingLength.Any())
            {
                return null;
            }

            return listBookingLength;
        }

        public IEnumerable<RoomsDto> GetFreeRoom()
        {
            var roomBooking = from room in _roomRepository.GetAll()
                              join booking in GetAll() on room.Guid equals booking.Guid into bookingGroup
                              from booking in bookingGroup.DefaultIfEmpty()
                              where booking == null || booking.EndDate < DateTime.Now || booking.RoomGuid != room.Guid
                              select new RoomsDto
                              {
                                  Guid = room.Guid,
                                  Name = room.Name,
                                  Capacity = room.Capacity,
                                  Floor = room.Floor
                              };
            if (!roomBooking.Any())
            {
                return null;
            }

            return roomBooking;
        }
        public IEnumerable<DetailBookingDto> GetAllDetailBooking()
        {
            var getDetail = (from booking in _bookingRepository.GetAll()
                             join room in _roomRepository.GetAll() on booking.RoomGuid equals room.Guid
                             join emplooyee in _employeeRepository.GetAll() on booking.EmployeeGuid equals emplooyee.Guid
                             select new DetailBookingDto
                             {
                                 BookingGuid = booking.Guid,
                                 BookedNik = emplooyee.NIK,
                                 BookedBy = emplooyee.FirstName + " " + emplooyee.LastName,
                                 RoomName = room.Name,
                                 StartDate = booking.StartDate,
                                 EndDate = booking.EndDate,
                                 Status = booking.Status,
                                 Remarks = booking.Remarks
                             });
            return getDetail;
        }

        public DetailBookingDto? GetAllDetailBookingByGuid(Guid guid)
        {
            return GetAllDetailBooking().SingleOrDefault(b => b.BookingGuid == guid);
        }

        public IEnumerable<BookingDto> GetAll()
        {
            var book = _bookingRepository.GetAll();
            if (!book.Any())
            {
                return Enumerable.Empty<BookingDto>();
            }

            var bookDto = new List<BookingDto>();
            foreach (var books in book)
            {
                bookDto.Add((BookingDto)books);
            }
            return bookDto;
        }

        public BookingDto? GetByGuid(Guid guid)
        {
            var book = _bookingRepository.GetByGuid(guid);
            if (book is null)
            {
                return null;
            }
            return (BookingDto)book;
        }

        public BookingDto? Create(NewBookingDto newBookingDto)
        {
            var book = _bookingRepository.Create(newBookingDto);
            if (book == null)
            {
                return null;
            }
            return (BookingDto)book;
        }

        public int Update(BookingDto bookingDto)
        {
            var book = _bookingRepository.GetByGuid(bookingDto.Guid);
            if (book is null)
            {
                return -1;
            }
            Booking toUpdate = bookingDto;
            toUpdate.CreatedDate = book.CreatedDate;
            var result = _bookingRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var book = _bookingRepository.GetByGuid(guid);
            if (book is null)
            {
                return -1;
            }

            var result = _bookingRepository.Delete(book);

            return result ? 1 : 0;
        }
    }
}
