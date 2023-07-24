using APIMCC.DTOs.AccountRoles;
using FluentValidation;

namespace APIMCC.Utilities.Validations.AccountRole
{
    public class UpdateAccountRoleValidator : AbstractValidator<AccountRoleDto>
    {
        public UpdateAccountRoleValidator()
        {
            RuleFor(ar => ar.RoleGuid).NotNull();

            RuleFor(ar => ar.AccountGuid).NotNull();
        }
    }
}
