namespace TravelTracker.Core.Models.EmployeeModels
{
    public record EmployeeResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string MiddleName,
        string Position,
        string Department
        );
}
