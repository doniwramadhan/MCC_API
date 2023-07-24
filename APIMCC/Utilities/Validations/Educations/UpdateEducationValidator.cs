using APIMCC.DTOs.Educations;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Educations
{
    public class UpdateEducationValidator : AbstractValidator<EducationDto>
    {
        public UpdateEducationValidator()
        {
            RuleFor(ed => ed.Guid)
                .NotEmpty().WithMessage("Cannot be empty, input your Employee Guid");

            RuleFor(ed => ed.UniversityGuid)
                .NotEmpty().WithMessage("Cannot be empty, input University Guid");

            RuleFor(ed => ed.Major)
                .NotEmpty();

            RuleFor(ed => ed.Degree)
                .NotEmpty();

            RuleFor(ed => ed.GPA)
                .NotEmpty();
        }
    }
}
