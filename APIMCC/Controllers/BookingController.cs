using APIMCC.DTOs.Bookings;
using APIMCC.DTOs.Rooms;
using APIMCC.Services;
using APIMCC.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIMCC.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }



        [HttpGet("length")]
        public IActionResult GetBookLenght()
        {
            var result = _bookingService.GetBookingLength();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<BookingLengthDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Room not found"
                });
            }

            return Ok(
                new ResponseHandler<IEnumerable<BookingLengthDto>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Success retrieving data",
                    Data = result
                });
        }



        [HttpGet("free-room")]
        public IActionResult GetFreeRoomToday()
        {
            var result = _bookingService.GetFreeRoom();
            if (result == null)
            {
                return NotFound(new ResponseHandler<IEnumerable<RoomsDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Room is not found"
                });
                
            }
            return Ok(new ResponseHandler<IEnumerable<RoomsDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes retrieve data",
                Data = result
            });

        }

        [HttpGet("detail")]
        public IActionResult GetBookingDetail()
        {
            var result = _bookingService.GetAllDetailBooking();
            if (result is null)
            {
                return NotFound(new ResponseHandler<IEnumerable<DetailBookingDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
                return Ok(new ResponseHandler<IEnumerable<DetailBookingDto>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            
        }
        [HttpGet("detail/{guid}")]
        public IActionResult GetBookingDetailByGuid(Guid guid)
        {
            var result = _bookingService.GetAllDetailBookingByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<DetailBookingDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<DetailBookingDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingService.GetAll();
            if (result is null)
            {
                return NotFound(new ResponseHandler<IEnumerable<BookingDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<BookingDto>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            }
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<BookingDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            }
        }

        [HttpPost]
        public IActionResult Insert(NewBookingDto newBookingDto)
        {
            var result = _bookingService.Create(newBookingDto);
            if (result is null)
            {
                return NotFound(new ResponseHandler<NewBookingDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes insert data",
                    Data = result
                });
            }
        }

        [HttpPut]
        public IActionResult Update(BookingDto bookingDto)
        {
            var result = _bookingService.Update(bookingDto);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<BookingDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Update is failed"
                });
            }

            return Ok(new ResponseHandler<BookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes update data",
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _bookingService.Delete(guid);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<BookingDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Delete is failed"
                });
            }
            return Ok(new ResponseHandler<BookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes delete data",
            });
        }
    }
}
