using APIMCC.Contracts;
using APIMCC.DTOs.Universities;
using APIMCC.Models;
using FluentValidation;

namespace APIMCC.Utilities.Validations.Universities
{
    public class NewUniversityValidator : AbstractValidator<NewUniversityDto>
    {
        private readonly IUniversityRepository _universityRepository;
        public NewUniversityValidator(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;

            RuleFor(u => u.Code)
                .NotNull()
                .MaximumLength(5);
                
                
            RuleFor(u => u.Name)
                .NotEmpty();
        }

        
    }
}
