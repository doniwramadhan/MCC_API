using APIMCC.DTOs.AccountRoles;
using APIMCC.Services;
using APIMCC.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/account-roles")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private readonly AccountRoleService _accountRoleService;

        public AccountRoleController(AccountRoleService accountRoleService)
        {
            _accountRoleService = accountRoleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRoleService.GetAll();
            if (result is null)
            {
                return NotFound(new ResponseHandler<IEnumerable<AccountRoleDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<AccountRoleDto>>
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
            var result = _accountRoleService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<AccountRoleDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<AccountRoleDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            }
        }

        [HttpPost]
        public IActionResult Insert(NewAccountRoleDto newAccountRoleDto)
        {
            var result = _accountRoleService.Create(newAccountRoleDto);
            if (result is null)
            {
                return NotFound(new ResponseHandler<NewAccountRoleDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<AccountRoleDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes insert data",
                    Data = result
                });
            }
        }

        [HttpPut]
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            var result = _accountRoleService.Update(accountRoleDto);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<AccountRoleDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<AccountRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Update is failed"
                });
            }

            return Ok(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes update data",
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _accountRoleService.Delete(guid);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<AccountRoleDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<AccountRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Delete is failed"
                });
            }
            return Ok(new ResponseHandler<AccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes delete data",
            });
        }
    }
}
