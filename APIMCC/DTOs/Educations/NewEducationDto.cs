using APIMCC.Models;

namespace APIMCC.DTOs.Educations
{
    public class NewEducationDto
    {
        public Guid Guid { get; set; }
        public Guid UniversityGuid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }

        public static implicit operator Education(NewEducationDto newEducationDto)
        {
            return new Education
            {
                Guid = newEducationDto.Guid,
                UniversityGuid = newEducationDto.UniversityGuid,
                Major = newEducationDto.Major,
                Degree = newEducationDto.Degree,
                GPA = newEducationDto.GPA,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewEducationDto(Education education)
        {
            return new NewEducationDto
            {
                Guid = education.Guid,
                UniversityGuid = education.UniversityGuid,
                Major = education.Major,
                Degree = education.Degree,
                GPA = education.GPA,
            };
        }
    }
}
