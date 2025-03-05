using TravelTracker.Core.Models.AdvanceReportModels;
using TravelTracker.Core.Models.BusinessTripModels;
using TravelTracker.Core.Models.CityModels;
using TravelTracker.Core.Models.CommandModels;
using TravelTracker.Core.Models.EmployeeModels;
using TravelTracker.Core.Models.TravelExpenseTypeModels;
using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.Application.Services
{
    public interface IValidationService
    {
        Dictionary<string, string> Validation(AdvanceReportEntity advanceReport);
        Dictionary<string, string> Validation(CityEntity city);
        Dictionary<string, string> Validation(CommandEntity command);
        Dictionary<string, string> Validation(EmployeeEntity employee);
        Dictionary<string, string> Validation(TripCertificateEntity tripCertificate);
        Dictionary<string, string> Validation(TripExpenseEntity tripExpense);
        Dictionary<string, string> Validation(TripExpenseTypeEntity tripExpenseType);
    }
}