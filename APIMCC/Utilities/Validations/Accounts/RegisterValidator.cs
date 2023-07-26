using APIMCC.Contracts;
using APIMCC.DTOs.Accounts;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public RegisterValidator(IEmployeeRepository employeeRepository)
        {
           _employeeRepository = employeeRepository;

            //Employee Data
            _employeeRepository = employeeRepository;

            RuleFor(e => e.FirstName)
                .NotEmpty();

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-20));

            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();

            RuleFor(e => e.HireDate)
                .NotEmpty();

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .Must(IsDuplicateValue).WithMessage("Email already exists");

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches("^(^\\+62|62|^08)(\\d{3,4}-?){2}\\d{3,4}$")
                .Must(IsDuplicateValue).WithMessage("Phone Number already exists");

            //University Data
            RuleFor(u => u.Name)
                .NotEmpty();

            RuleFor(u => u.Code)
                .NotEmpty();

            //Education Data
            RuleFor(e => e.Major)
                .NotEmpty();

            RuleFor(e => e.Degree)
                .NotEmpty();

            RuleFor(e => e.GPA)
                .NotEmpty();

            //Account Data
            RuleFor(pass => pass.Password)
                .NotEmpty()
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");

            RuleFor(pass => pass.ConfirmPassowrd)
                .Equal(pass => pass.Password)
                .WithMessage("Passwords do not match");
        }

        private bool IsDuplicateValue(string value)
        {
            return _employeeRepository.IsNotExist(value);
        }
    }
}
