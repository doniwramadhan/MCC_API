using APIMCC.Contracts;
using APIMCC.DTOs.Employees;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Employees
{
    public class UpdateEmployeeValidator : AbstractValidator<EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(e => e.FirstName).NotEmpty();

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();

            RuleFor(e => e.HireDate)
                .NotEmpty();

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .Must(IsDuplicateValue).WithMessage("Email is already exist");

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(@"^\+[0-9]+$").WithMessage("Format phone number using +62")
                .Must(IsDuplicateValue).WithMessage("Phone number is already exist");
        }

        private bool IsDuplicateValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }
    }
}
