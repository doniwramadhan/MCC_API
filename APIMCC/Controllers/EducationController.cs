using APIMCC.Contracts;
using APIMCC.DTOs.Educations;
using APIMCC.DTOs.Employees;
using APIMCC.Models;
using APIMCC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/educations")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly EducationService _educationService;

        public EducationController(EducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _educationService.GetAll();
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
            var result = _educationService.GetByGuid(guid);
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
        public IActionResult Insert(NewEducationDto newEducationDto)
        {
            var result = _educationService.Create(newEducationDto);
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
        public IActionResult Update(EducationDto educationDto)
        {
            var result = _educationService.Update(educationDto);
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
            var result = _educationService.Delete(guid);
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
