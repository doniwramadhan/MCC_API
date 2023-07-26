using APIMCC.DTOs.Educations;
using APIMCC.DTOs.Universities;
using APIMCC.Models;
using APIMCC.Utilities.Enums;

namespace APIMCC.DTOs.Employees
{
    public class MasterEmployeeDto
    {
        //Employee data
        public Guid EmployeeGuid { get; set; }
        public string NIK { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        //Education data
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }

        //University data
        public string UniversityName { get; set; }


    }
}
