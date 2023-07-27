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
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }

        //University input
        public string Code { get; set; }
        public string Name { get; set; }

        //Account input
        public string Password { get; set; }
        public string ConfirmPassowrd { get; set; }

        //public int OTP { get; set; }
        //public bool IsUsed { get; set; }
        //public DateTime ExpiredDate { get; set; }

    }
}
