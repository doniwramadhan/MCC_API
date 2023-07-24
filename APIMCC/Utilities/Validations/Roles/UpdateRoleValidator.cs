using APIMCC.DTOs.Roles;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Roles
{
    public class UpdateRoleValidator : AbstractValidator<RoleDto>
    {
        public UpdateRoleValidator()
        {
            RuleFor(role => role.Name).NotEmpty();
        }
    }
}
