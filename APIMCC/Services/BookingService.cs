using APIMCC.Contracts;
using APIMCC.DTOs.Bookings;
using APIMCC.DTOs.Educations;
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
        public IEnumerable<DetailBookingDto> GetAllDetailBooking()
        {
            var getDetail = (from booking in _bookingRepository.GetAll()
                             join room in _roomRepository.GetAll() on booking.RoomGuid equals room.Guid
                             join emplooyee in _employeeRepository.GetAll() on booking.EmployeeGuid equals emplooyee.Guid
                             select new DetailBookingDto
                             {
                                 BookingGuid = emplooyee.Guid,
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
