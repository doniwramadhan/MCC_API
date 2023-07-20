using APIMCC.Contracts;
using APIMCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
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
            var result = _employeeRepository.GetByGuid(guid);
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
        public IActionResult Insert(Employee employee)
        {
            var result = _employeeRepository.Create(employee);
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
        public IActionResult Update(Employee employee)
        {
            var check = _employeeRepository.GetByGuid(employee.Guid);
            if (check is null)
            {
                return NotFound("Guid is not found");
            }

            var result = _employeeRepository.Update(employee);
            if (!result)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Update succes");
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _employeeRepository.GetByGuid(guid);
            if (data is null)
            {
                return NotFound("Guid is not found");
            }

            var result = _employeeRepository.Delete(data);
            if (!result)
            {
                return StatusCode(500, "Error from database");
            }

            return Ok("Delete succes");
        }
    }
}
