using APIMCC.Contracts;
using APIMCC.DTOs.AccountRoles;
using APIMCC.DTOs.Universities;
using APIMCC.Models;
using APIMCC.Services;
using Microsoft.AspNetCore.Http;
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
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRoleService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public IActionResult Insert(NewAccountRoleDto newAccountRoleDto)
        {
            var result = _accountRoleService.Create(newAccountRoleDto);
            if (result is null)
            {
                return StatusCode(500, "Error from database");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPut]
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            var result = _accountRoleService.Update(accountRoleDto);
            if (result is 0)
            {
                return NotFound();
            }
            if (result is -1)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Update succes");
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _accountRoleService.Delete(guid);
            if (result is 0)
            {
                return NotFound();
            }
            if (result is -1)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Delete succes");
        }
    }
}
