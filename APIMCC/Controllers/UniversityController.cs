using APIMCC.DTOs.Universities;
using APIMCC.Services;
using APIMCC.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/universities")]
    [ApiController]
    public class UniversityController : ControllerBase
    {

        private readonly UniversityService _universityService;

        public UniversityController(UniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universityService.GetAll();
            if (result is null)
            {
                return NotFound(new ResponseHandler<IEnumerable<UniversityDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<UniversityDto>>
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
            var result = _universityService.GetByGuid(guid);
            if(result is null)
            {
                return NotFound(new ResponseHandler<UniversityDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Guid is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            }
        }

        [HttpPost]
        public IActionResult Insert(NewUniversityDto newUniversityDto)
        {
            var result = _universityService.Create(newUniversityDto);
            if(result is null)
            {
                return StatusCode(500, new ResponseHandler<NewUniversityDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Insert is failed"
                });
            }
            else
            {
                return Ok(new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes insert data",
                    Data = result
                });
            }
        }

        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var result = _universityService.Update(universityDto);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<UniversityDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Guid is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Update is failed"
                });
            }

            return Ok(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes update data",
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _universityService.Delete(guid);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<UniversityDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Guid is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Delete is failed"
                });
            }

            return Ok(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Delete success",
            });
        }
    }
}
