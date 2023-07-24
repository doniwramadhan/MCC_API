using APIMCC.DTOs.Bookings;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Booking
{
    public class UpdateBookingValidator : AbstractValidator<BookingDto>
    {
        public UpdateBookingValidator()
        {
            RuleFor(b => b.EmployeeGuid).NotNull();

            RuleFor(b => b.RoomGuid).NotNull();

            RuleFor(b => b.StartDate).NotEmpty();

            RuleFor(b => b.EndDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now.AddDays(+1))
                .WithMessage("End date must be +1 day");

            RuleFor(b => b.Status)
                .NotNull()
                .IsInEnum();
        }
    }
}
