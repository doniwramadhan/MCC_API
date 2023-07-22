using APIMCC.Contracts;
using APIMCC.DTOs.Educations;
using APIMCC.DTOs.Roles;
using APIMCC.Models;
using APIMCC.Repositories;

namespace APIMCC.Services
{
    public class EducationService
    {
        private readonly IEducationRepository _educationRepository;

        public EducationService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public IEnumerable<EducationDto> GetAll()
        {
            var edu = _educationRepository.GetAll();
            if (!edu.Any())
            {
                return Enumerable.Empty<EducationDto>();
            }

            var eduDto = new List<EducationDto>();
            foreach (var edus in edu)
            {
                eduDto.Add((EducationDto)edus);
            }
            return eduDto;
        }

        public EducationDto? GetByGuid(Guid guid)
        {
            var edu = _educationRepository.GetByGuid(guid);
            if (edu is null)
            {
                return null;
            }
            return (EducationDto)edu;
        }

        public EducationDto? Create(NewEducationDto newEducationDto)
        {
            var edu = _educationRepository.Create(newEducationDto);
            if (edu == null)
            {
                return null;
            }
            return (EducationDto)edu;
        }

        public int Update(EducationDto educationDto)
        {
            var edu = _educationRepository.GetByGuid(educationDto.Guid);
            if (edu is null)
            {
                return -1;
            }
            Education toUpdate = educationDto;
            toUpdate.CreatedDate = edu.CreatedDate;
            var result = _educationRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var edu = _educationRepository.GetByGuid(guid);
            if (edu is null)
            {
                return -1;
            }

            var result = _educationRepository.Delete(edu);

            return result ? 1 : 0;
        }
    }
}
