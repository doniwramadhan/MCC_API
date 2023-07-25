using APIMCC.DTOs.Rooms;
using APIMCC.Services;
using APIMCC.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomService.GetAll();
            if (result is null)
            {
                return NotFound(new ResponseHandler<IEnumerable<RoomsDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<RoomsDto>>
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
            var result = _roomService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<IEnumerable<RoomsDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Error",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<RoomsDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            }
        }

        [HttpPost]
        public IActionResult Insert(NewRoomsDto newRoomsDto)
        {
            var result = _roomService.Create(newRoomsDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewRoomsDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Insert is failed"
                });
            }
            else
            {
                return Ok(new ResponseHandler<RoomsDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes insert data",
                    Data = result
                });
            }
        }

        [HttpPut]
        public IActionResult Update(RoomsDto roomsDto)
        {
            var result = _roomService.Update(roomsDto);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<RoomsDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Error",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<RoomsDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Failed update data"
                });
            }

            return Ok(new ResponseHandler<RoomsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes update data",
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _roomService.Delete(guid);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<RoomsDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Error",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<RoomsDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Failed delete data"
                });
            }

            return Ok(new ResponseHandler<RoomsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes delete data",
            });
        }
    }
}
