using APIMCC.DTOs.Roles;
using APIMCC.DTOs.Rooms;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Rooms
{
    public class UpdateRoleValidator : AbstractValidator<RoomsDto>
    {
        public UpdateRoleValidator()
        {
            RuleFor(r => r.Name).NotEmpty();

            RuleFor(r => r.Floor).NotEmpty();

            RuleFor(r => r.Capacity).NotEmpty();
        }
    }
}
