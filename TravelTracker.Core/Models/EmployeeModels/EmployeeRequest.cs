namespace TravelTracker.Core.Models.EmployeeModels
{
    public record EmployeeRequest(
        string FirstName,
        string LastName,
        string MiddleName,
        string Position,
        string Department
        );
}
