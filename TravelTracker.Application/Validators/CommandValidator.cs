using FluentValidation;
using TravelTracker.Core.Models.CommandModels;

namespace TravelTracker.Application.Validators
{
    internal class CommandValidator : AbstractValidator<CommandEntity>
    {
        public CommandValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("Заголовок не может быть пустой.");

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Описание не может быть пустым.");

            RuleFor(e => e.DateIssued)
                .NotEmpty().WithMessage("Дата издания не может быть пустой.");
        }
    }
}
