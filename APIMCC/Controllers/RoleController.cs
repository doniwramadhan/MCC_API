using APIMCC.DTOs.Roles;
using APIMCC.Services;
using APIMCC.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleService.GetAll();
            if (result is null)
            {
                return NotFound(new ResponseHandler<IEnumerable<RoleDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<RoleDto>>
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
            var result = _roleService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<RoleDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            }
        }

        [HttpPost]
        public IActionResult Insert(NewRoleDto newRoleDto)
        {
            var result = _roleService.Create(newRoleDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Insert is failed"
                });
            }
            else
            {
                return Ok(new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes insert data",
                    Data = result
                });
            }
        }

        [HttpPut]
        public IActionResult Update(RoleDto roleDto)
        {
            var result = _roleService.Update(roleDto);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<RoleDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Update is failed"
                });
            }

            return Ok(new ResponseHandler<RoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes update data",
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _roleService.Delete(guid);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<RoleDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Delete is failed"
                });
            }
            return Ok(new ResponseHandler<RoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes delete data",
            });
        }
    }
}
