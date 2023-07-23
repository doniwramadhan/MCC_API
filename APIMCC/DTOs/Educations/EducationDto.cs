using APIMCC.DTOs.Employees;
using APIMCC.Models;

namespace APIMCC.DTOs.Educations
{
    public class EducationDto
    {
        public Guid Guid { get; set; }
        public Guid UniversityGuid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }

        public static implicit operator Education(EducationDto educationDto)
        {
            return new Education
            {
                Guid = educationDto.Guid,
                UniversityGuid = educationDto.UniversityGuid,
                Major = educationDto.Major,
                Degree = educationDto.Degree,
                GPA = educationDto. GPA,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator EducationDto(Education education)
        {
            return new EducationDto
            {
                Guid = education.Guid,
                UniversityGuid = education.UniversityGuid,
                Major = education.Major,
                Degree = education.Degree,
                GPA = education. GPA,
            };
        }
    }
}
