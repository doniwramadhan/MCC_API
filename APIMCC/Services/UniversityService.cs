using APIMCC.Contracts;
using APIMCC.DTOs.Universities;
using APIMCC.Models;

namespace APIMCC.Services
{
    public class UniversityService
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityService(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public IEnumerable<UniversityDto> GetAll()
        {
            var univ = _universityRepository.GetAll();
            if(!univ.Any())
            {
                return Enumerable.Empty<UniversityDto>();
            }

            var univDto = new List<UniversityDto>();
            foreach(var univer in univ)
            {
                univDto.Add((UniversityDto)univer);
            }
            return univDto;
        }

        public UniversityDto? GetByGuid(Guid guid)
        {
            var univ = _universityRepository.GetByGuid(guid);
            if(univ == null)
            {
                return null;
            }
            return (UniversityDto)univ;
        }

        public UniversityDto? Create(NewUniversityDto newUniversityDto)
        {
            var univ = _universityRepository.Create(newUniversityDto);
            if(univ == null)
            {
                return null;
            }
            return(UniversityDto)univ;
        }

        public int Update(UniversityDto universityDto)
        {
            var univ = _universityRepository.GetByGuid(universityDto.Guid);
            if(univ is null)
            {
                return -1;
            }
            University toUpdate = universityDto;
            toUpdate.CreatedDate = univ.CreatedDate;
            var result = _universityRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var university = _universityRepository.GetByGuid(guid);
            if (university is null)
            {
                return -1; 
            }

            var result = _universityRepository.Delete(university);

            return result ? 1 : 0; 
        }
    }
}
