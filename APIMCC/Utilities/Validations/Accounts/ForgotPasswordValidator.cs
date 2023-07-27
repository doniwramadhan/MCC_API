using APIMCC.Contracts;
using APIMCC.DTOs.Accounts;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Accounts
{
   
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordDto>
    {
        private readonly
IEmployeeRepository _employeeRepository;
        public ForgotPasswordValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(e => e.Email).NotEmpty();
        }
    }
}
