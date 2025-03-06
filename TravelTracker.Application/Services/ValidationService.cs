using FluentValidation.Results;
using TravelTracker.Application.Validators;
using TravelTracker.Core.Models.AdvanceReportModels;
using TravelTracker.Core.Models.BusinessTripModels;
using TravelTracker.Core.Models.CityModels;
using TravelTracker.Core.Models.CommandModels;
using TravelTracker.Core.Models.EmployeeModels;
using TravelTracker.Core.Models.TravelExpenseTypeModels;
using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.Application.Services
{
    public class ValidationService : IValidationService
    {
        public Dictionary<string, string> Validation(AdvanceReportEntity advanceReport)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            AdvanceReportValidator validations = new AdvanceReportValidator();
            ValidationResult validationResult = validations.Validate(advanceReport);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName.ToLower()] = failure.ErrorMessage;
                }
            }

            return errors;
        }

        public Dictionary<string, string> Validation(CityEntity city)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            CityValidator validations = new CityValidator();
            ValidationResult validationResult = validations.Validate(city);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName.ToLower()] = failure.ErrorMessage;
                }
            }

            return errors;
        }

        public Dictionary<string, string> Validation(CommandEntity command)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            CommandValidator validations = new CommandValidator();
            ValidationResult validationResult = validations.Validate(command);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName.ToLower()] = failure.ErrorMessage;
                }
            }

            return errors;
        }

        public Dictionary<string, string> Validation(EmployeeEntity employee)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            EmployeeValidator validations = new EmployeeValidator();
            ValidationResult validationResult = validations.Validate(employee);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName.ToLower()] = failure.ErrorMessage;
                }
            }

            return errors;
        }

        public Dictionary<string, string> Validation(TripCertificateEntity tripCertificate)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            TripCertificateValidator validations = new TripCertificateValidator();
            ValidationResult validationResult = validations.Validate(tripCertificate);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName.ToLower()] = failure.ErrorMessage;
                }
            }

            return errors;
        }

        public Dictionary<string, string> Validation(TripExpenseEntity tripExpense)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            TripExpenseValidator validations = new TripExpenseValidator();
            ValidationResult validationResult = validations.Validate(tripExpense);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName.ToLower()] = failure.ErrorMessage;
                }
            }

            return errors;
        }

        public Dictionary<string, string> Validation(TripExpenseTypeEntity tripExpenseType)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            TripExpenseTypeValidator validations = new TripExpenseTypeValidator();
            ValidationResult validationResult = validations.Validate(tripExpenseType);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errors[failure.PropertyName.ToLower()] = failure.ErrorMessage;
                }
            }

            return errors;
        }
    }
}
