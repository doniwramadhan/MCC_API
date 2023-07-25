using APIMCC.Contracts;
using APIMCC.DTOs.Universities;
using APIMCC.Models;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Universities
{
    public class NewUniversityValidator : AbstractValidator<NewUniversityDto>
    {
        public NewUniversityValidator()
        {

            RuleFor(u => u.Code)
                .NotEmpty()
                .MaximumLength(5);
                
                
            RuleFor(u => u.Name)
                .NotEmpty();
        }

        
    }
}
