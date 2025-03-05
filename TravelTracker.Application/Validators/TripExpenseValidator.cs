using FluentValidation;
using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.Application.Validators
{
    public class TripExpenseValidator : AbstractValidator<TripExpenseEntity>
    {
        public TripExpenseValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма не должна быть меньше 0.");

            RuleFor(x => x.Date)
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Дата должна быть в формате yyyy-MM-dd.");
        }
    }
}
