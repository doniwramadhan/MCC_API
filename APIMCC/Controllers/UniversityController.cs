using APIMCC.Contracts;
using APIMCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMCC.Controllers
{
    [Route("api/universities")]
    [ApiController]
    public class UniversityController : ControllerBase
    {

        private readonly IUniversityRepository _universityRepository;
        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universityRepository.GetAll();
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
            var result = _universityRepository.GetByGuid(guid);
            if(result is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public IActionResult Insert(University univerity)
        {
            var result = _universityRepository.Create(univerity);
            if(result is null)
            {
                return StatusCode(500, "Error from database");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPut]
        public IActionResult Update(University university)
        {
            var check = _universityRepository.GetByGuid(university.Guid);
            if(check is null)
            {
                return NotFound("Guid is not found");
            }

            var result = _universityRepository.Update(university);
            if (!result)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Update succes");
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _universityRepository.GetByGuid(guid);
            if(data is null)
            {
                return NotFound("Guid is not found");
            }

            var result = _universityRepository.Delete(data);
            if (!result)
            {
                return StatusCode(500, "Error from database");
            }

            return Ok("Delete succes");
        }
    }
}
