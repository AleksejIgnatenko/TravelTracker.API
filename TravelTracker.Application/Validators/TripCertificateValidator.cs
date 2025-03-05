using FluentValidation;
using TravelTracker.Core.Models.BusinessTripModels;

namespace TravelTracker.Application.Validators
{
    internal class TripCertificateValidator : AbstractValidator<TripCertificateEntity>
    {
        public TripCertificateValidator()
        {
            RuleFor(x => x.StartDate)
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Дата начала командировки должно быть в формате yyyy-MM-dd.");

            RuleFor(x => x.EndDate)
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Дата окончания командировки должно быть в формате yyyy-MM-dd.");
        }
    }
}
