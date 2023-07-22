using APIMCC.Contracts;
using APIMCC.DTOs.Employees;
using APIMCC.DTOs.Rooms;
using APIMCC.Models;
using APIMCC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeService.GetAll();
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
            var result = _employeeService.GetByGuid(guid);
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
        public IActionResult Insert(NewEmployeeDto newEmployeeDto)
        {
            var result = _employeeService.Create(newEmployeeDto);
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
        public IActionResult Update(EmployeeDto employeeDto)
        {
            var result = _employeeService.Update(employeeDto);
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
            var result = _employeeService.Delete(guid);
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
