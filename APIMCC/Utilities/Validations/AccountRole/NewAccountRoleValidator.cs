using APIMCC.DTOs.AccountRoles;
using APIMCC.DTOs.Accounts;
using FluentValidation;

namespace APIMCC.Utilities.Validations.AccountRole
{
    public class NewAccountRoleValidator : AbstractValidator<NewAccountRoleDto>
    {
        public NewAccountRoleValidator()
        {
            RuleFor(ar => ar.RoleGuid).NotNull();

            RuleFor(ar => ar.AccountGuid).NotNull();
        }
    }
}
