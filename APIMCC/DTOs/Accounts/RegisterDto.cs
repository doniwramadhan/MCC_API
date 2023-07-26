using APIMCC.DTOs.Educations;
using APIMCC.DTOs.Employees;
using APIMCC.DTOs.Universities;
using APIMCC.Models;
using APIMCC.Utilities.Enums;

namespace APIMCC.DTOs.Accounts
{
    public class RegisterDto
    {
        //Employee input
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        //Education input
        public Guid EmployeeGuid { get; set; }
        public Guid UniversityGuid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }

        //University input
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid EmployeGuidAccount { get; set; }
        public string Password { get; set; }
        public string ConfirmPassowrd { get; set; }

        //public int OTP { get; set; }
        //public bool IsUsed { get; set; }
        //public DateTime ExpiredDate { get; set; }


        public static implicit operator Employee(RegisterDto registerDto)
        {
            return new Employee
            {
                Guid = new Guid(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                HireDate = registerDto.HireDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }

        public static implicit operator Education(RegisterDto registerDto)
        {
            return new Education
            {
                Guid = registerDto.EmployeeGuid,
                UniversityGuid = registerDto.UniversityGuid,
                Major = registerDto.Major,
                Degree = registerDto.Degree,
                GPA = registerDto.GPA,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static implicit operator University(RegisterDto registerDto)
        {
            return new University
            {
                Guid = new Guid(),
                Code = registerDto.Code,
                Name = registerDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static implicit operator Account(RegisterDto registerDto)
        {
            return new Account
            {
                Guid = registerDto.EmployeGuidAccount,
                OTP = 111,
                IsUsed = true,
                Password = registerDto.Password,
                ExpiredDate = DateTime.Now.AddYears(5),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
