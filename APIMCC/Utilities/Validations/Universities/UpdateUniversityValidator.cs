using APIMCC.DTOs.Universities;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Universities
{
    public class UpdateUniversityValidator : AbstractValidator<UniversityDto>
    {
        public UpdateUniversityValidator()
        {
            RuleFor(u => u.Code)
                .NotEmpty()
                .MaximumLength(5);


            RuleFor(u => u.Name)
                .NotEmpty();
        }
    }
}
