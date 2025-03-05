using FluentValidation;
using TravelTracker.Core.Models.EmployeeModels;

namespace TravelTracker.Application.Validators
{
    internal class EmployeeValidator : AbstractValidator<EmployeeEntity>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty().Matches("^[а-яА-Яa-zA-Z]*$").WithMessage("Имя должно содержать только буквы.");
            
            RuleFor(e => e.LastName)
                .NotEmpty().Matches("^[а-яА-Яa-zA-Z]+( [а-яА-Яa-zA-Z]+)*$").WithMessage("Фамилия должна содержать только буквы и допускать двойные фамилии.");
            
            RuleFor(e => e.MiddleName)
                .NotEmpty().Matches("^[а-яА-Яa-zA-Z]*$").WithMessage("Отчество должно содержать только буквы.");
            
            RuleFor(e => e.Position)
                .NotEmpty().WithMessage("Должность не может быть пустой.");
           
            RuleFor(e => e.Department)
                .NotEmpty().WithMessage("Отдел не может быть пустым.");
        }
    }
}
