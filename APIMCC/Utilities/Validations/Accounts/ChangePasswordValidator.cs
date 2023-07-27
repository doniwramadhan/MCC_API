using APIMCC.Contracts;
using APIMCC.DTOs.Accounts;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Accounts
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public ChangePasswordValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(ac => ac.Email)
            .NotEmpty().WithMessage("Email is required");
            RuleFor(ac => ac.OTP)
               .NotEmpty().WithMessage("OTP is required");

            RuleFor(ac => ac.NewPassword)
               .NotEmpty().WithMessage("Password is required")
              .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");

            RuleFor(ac => ac.ConfirmPassword)
                .Equal(ac => ac.NewPassword).WithMessage("Password Correct")
             .WithMessage("Passwords do not match");
        }
    }
}
