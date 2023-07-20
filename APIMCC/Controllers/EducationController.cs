using APIMCC.Contracts;
using APIMCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/educations")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;
        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _educationRepository.GetAll();
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
            var result = _educationRepository.GetByGuid(guid);
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
        public IActionResult Insert(Education univerity)
        {
            var result = _educationRepository.Create(univerity);
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
        public IActionResult Update(Education Education)
        {
            var check = _educationRepository.GetByGuid(Education.Guid);
            if (check is null)
            {
                return NotFound("Guid is not found");
            }

            var result = _educationRepository.Update(Education);
            if (!result)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Update succes");
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _educationRepository.GetByGuid(guid);
            if (data is null)
            {
                return NotFound("Guid is not found");
            }

            var result = _educationRepository.Delete(data);
            if (!result)
            {
                return StatusCode(500, "Error from database");
            }

            return Ok("Delete succes");
        }
    }
}
