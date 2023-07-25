using APIMCC.Models;
using APIMCC.Utilities.Enums;

namespace APIMCC.DTOs.Employees
{
    public class UpdateEmployeeDto
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        public static implicit operator Employee(UpdateEmployeeDto updateEmployeeDto)
        {
            return new Employee
            {
                Guid = updateEmployeeDto.Guid,
                FirstName = updateEmployeeDto.FirstName,
                LastName = updateEmployeeDto.LastName,
                BirthDate = updateEmployeeDto.BirthDate,
                Gender = updateEmployeeDto.Gender,
                Email = updateEmployeeDto.Email,
                PhoneNumber = updateEmployeeDto.PhoneNumber,
                HireDate = updateEmployeeDto.HireDate,
                ModifiedDate = DateTime.Now

            };
        }
    }
}
