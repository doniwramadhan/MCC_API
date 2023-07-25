using APIMCC.DTOs.Accounts;
using APIMCC.Services;
using APIMCC.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountService.GetAll();
            if (result is null)
            {
                return NotFound(new ResponseHandler<IEnumerable<AccountDto>>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<IEnumerable<AccountDto>>
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
            var result = _accountService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<AccountDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            else
            {
                return Ok(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes retrieve data",
                    Data = result
                });
            }
        }

        [HttpPost]
        public IActionResult Insert(NewAccountDto newAccountDto)
        {
            var result = _accountService.Create(newAccountDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Insert is failed"
                });
            }
            else
            {
                return Ok(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = "OK",
                    Message = "Succes insert data",
                    Data = result
                });
            }
        }

        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            var result = _accountService.Update(accountDto);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<AccountDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Update is failed"
                });
            }

            return Ok(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes update data",
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _accountService.Delete(guid);
            if (result is 0)
            {
                return NotFound(new ResponseHandler<AccountDto>()
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = "Not Found",
                    Message = "Data is not found"
                });
            }
            if (result is -1)
            {
                return StatusCode(500, "Error Retrieve from database");
            }
            return Ok(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes delete data",
            });
        }
    }
}
