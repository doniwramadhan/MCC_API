using APIMCC.Contracts;
using APIMCC.DTOs.Bookings;
using APIMCC.DTOs.Educations;
using APIMCC.Models;

namespace APIMCC.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
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
