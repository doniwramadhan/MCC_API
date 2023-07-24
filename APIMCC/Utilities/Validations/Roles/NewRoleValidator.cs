using APIMCC.DTOs.Roles;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Roles
{
    public class NewRoleValidator : AbstractValidator<NewRoleDto>
    {
        public NewRoleValidator()
        {
            RuleFor(role => role.Name).NotEmpty();
        }
    }
}
