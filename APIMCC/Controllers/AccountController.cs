using APIMCC.DTOs.Accounts;
using APIMCC.Services;
using APIMCC.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var result = _accountService.Register(registerDto);
            if (result == null)
            {
                return StatusCode(500, new ResponseHandler<RegisterDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Register is failed"
                });
            }

            return Ok(new ResponseHandler<RegisterDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Register Success",
                Data = result
            });

        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _accountService.Login(loginDto);
            if(result is 0)
            {
                return NotFound(new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email or Password is incorrect"
                });
            }

            return Ok(new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success"
            });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var isUpdated = _accountService.ForgotPasswordDto(forgotPasswordDto);
            if(isUpdated is 0)
            {
                return NotFound(new ResponseHandler<ForgotPasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email is not found"
                });
            }

            if(isUpdated is -1)
            {
                return StatusCode(500, new ResponseHandler<ForgotPasswordDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = "Error",
                    Message = "Error from database"
                });
            }
            return Ok(new ResponseHandler<ForgotPasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes retrieve data",
                Data = forgotPasswordDto
            });

        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var update = _accountService.ChangePassword(changePasswordDto);
            if(update is -1)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email is not found"
                });
            }
            if(update is 0)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status406NotAcceptable,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Password is not match"
                });
            }
            if(update is 1)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status403Forbidden,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "OTP is used"
                });
            }
            if(update is 2)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status419AuthenticationTimeout,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "OTP is expired"
                });
            }

            return Ok(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = "OK",
                Message = "Succes retrieve data",
                Data = changePasswordDto
            });
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
