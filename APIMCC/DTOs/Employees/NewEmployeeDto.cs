using APIMCC.Models;
using APIMCC.Utilities.Enums;

namespace APIMCC.DTOs.Employees
{
    public class NewEmployeeDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        public static implicit operator Employee(NewEmployeeDto newEmployeeDto)
        {
            return new Employee
            {
                Guid = new Guid(),
                FirstName = newEmployeeDto.FirstName,
                LastName = newEmployeeDto.LastName,
                BirthDate = newEmployeeDto.BirthDate,
                Gender = newEmployeeDto.Gender,
                Email = newEmployeeDto.Email,
                PhoneNumber = newEmployeeDto.PhoneNumber,
                HireDate = newEmployeeDto.HireDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }

    }
}
