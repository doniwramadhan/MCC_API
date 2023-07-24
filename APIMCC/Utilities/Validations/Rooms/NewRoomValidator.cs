using APIMCC.DTOs.Rooms;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Rooms
{
    public class NewRoomValidator : AbstractValidator<NewRoomsDto>
    {
        public NewRoomValidator()
        {
            RuleFor(r => r.Name).NotEmpty();

            RuleFor(r => r.Floor).NotEmpty();

            RuleFor(r => r.Capacity).NotEmpty();
        }
    }
}
