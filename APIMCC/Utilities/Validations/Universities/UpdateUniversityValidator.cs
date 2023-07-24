using APIMCC.DTOs.Universities;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Universities
{
    public class UpdateUniversityValidator : AbstractValidator<UniversityDto>
    {
        public UpdateUniversityValidator()
        {
            RuleFor(u => u.Code)
                .NotNull()
                .MaximumLength(5);


            RuleFor(u => u.Name)
                .NotEmpty();
        }
    }
}
