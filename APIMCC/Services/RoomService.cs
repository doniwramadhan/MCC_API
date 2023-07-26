using APIMCC.Contracts;
using APIMCC.DTOs.Rooms;
using APIMCC.DTOs.Universities;
using APIMCC.Models;

namespace APIMCC.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public RoomService(IRoomRepository roomRepository, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<BookedRoomDto> GetRoom()
        {
            var today = DateTime.Today.ToString("dd-MM-yyyy");
            var book = _bookingRepository.GetAll().Where(b => b.StartDate.ToString("dd-MM-yyyy").Equals(today));
            
            if(!book.Any())
            {
                return null;
            }

            var bookToday = new List<BookedRoomDto>();

            foreach( var booking in book)
            {
                var employee = _employeeRepository.GetByGuid(booking.EmployeeGuid);
                var room = _roomRepository.GetByGuid(booking.RoomGuid);

                BookedRoomDto bookedRoomDto = new BookedRoomDto
                {
                    BookingGuid = booking.Guid,
                    RoomName = room.Name,
                    Status = booking.Status,
                    Floor = room.Floor,
                    BookedBy = employee.FirstName +" "+employee.LastName
                };

                bookToday.Add(bookedRoomDto);
            }
            return bookToday;
        }

        public IEnumerable<RoomsDto> GetAll()
        {
            var room = _roomRepository.GetAll();
            if(!room.Any())
            {
                return Enumerable.Empty<RoomsDto>();
            }

            var roomDto = new List<RoomsDto>();
            foreach (var rooms in room)
            {
                roomDto.Add((RoomsDto)rooms);
            }
            return roomDto;
        }

        public RoomsDto? GetByGuid(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if(room == null)
            {
                return null;
            }
            return(RoomsDto)room ;
        }

        public RoomsDto? Create(NewRoomsDto newRoomsDto)
        {
            var room = _roomRepository.Create(newRoomsDto);
            if(room == null)
            {
                return null;
            }
            return(RoomsDto)room;
        }

        public int Update(RoomsDto roomsDto)
        {
            var room = _roomRepository.GetByGuid(roomsDto.Guid);
            if(room is null)
            {
                return -1;
            }
            Room toUpdate = roomsDto;
            toUpdate.CreatedDate = room.CreatedDate;
            var result = _roomRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return -1; 
            }

            var result = _roomRepository.Delete(room);

            return result ? 1 : 0; 
        }
    }
}
