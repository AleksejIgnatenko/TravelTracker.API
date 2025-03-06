using FluentValidation;
using TravelTracker.Core.Models.AdvanceReportModels;

namespace TravelTracker.Application.Validators
{
    internal class AdvanceReportValidator : AbstractValidator<AdvanceReportEntity>
    {
        public AdvanceReportValidator()
        {
            RuleFor(x => x.DateOfDelivery)
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Дата сдачи авансового отчета должна быть в формате yyyy-MM-dd.");
        }
    }
}
