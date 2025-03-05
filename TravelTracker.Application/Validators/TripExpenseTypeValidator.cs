using FluentValidation;
using TravelTracker.Core.Models.TravelExpenseTypeModels;

namespace TravelTracker.Application.Validators
{
    internal class TripExpenseTypeValidator : AbstractValidator<TripExpenseTypeEntity>
    {
        public TripExpenseTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не может быть пустым.");

            RuleFor(x => x.Standard)
                .GreaterThan(0).WithMessage("Норма должна быть больше 0.");
        }
    }
}
