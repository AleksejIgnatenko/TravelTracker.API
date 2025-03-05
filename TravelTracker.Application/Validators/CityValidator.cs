using FluentValidation;
using TravelTracker.Core.Models.CityModels;

namespace TravelTracker.Application.Validators
{
    internal class CityValidator : AbstractValidator<CityEntity>
    {
        public CityValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Страна не может быть пустой.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Города не может быть пустой.");
        }
    }
}
