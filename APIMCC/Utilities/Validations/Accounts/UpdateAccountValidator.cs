using APIMCC.DTOs.Accounts;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Accounts
{
    public class UpdateAccountValidator :AbstractValidator<AccountDto>
    {
        public UpdateAccountValidator()
        {
            RuleFor(ac => ac.Guid)
                .NotNull()
                .WithMessage("Cannot be empty, input your Employee Guid");

            RuleFor(ac => ac.Password)
                .MinimumLength(8).WithMessage("Password at least have 8")
                .NotNull()
                .Matches(@"^(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])")
                .WithMessage("Password must contain at least one Upper letter, one digit number and one special character");

            RuleFor(ac => ac.OTP).NotNull();

            RuleFor(ac => ac.IsUsed).NotNull().WithMessage("Cannot be empty, only true or false");

            RuleFor(ac => ac.ExpiredDate).NotNull();
        }
    }
}
