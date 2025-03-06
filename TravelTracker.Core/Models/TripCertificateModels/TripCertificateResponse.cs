namespace TravelTracker.Core.Models.TripCertificateModels
{
    public record TripCertificateResponse (
        Guid Id,
        string Name,
        Guid EmployeeId,
        string EmployeeFullName,
        Guid CommandId,
        string CommandTitle,
        Guid CityId,
        string CityName,
        string StartDate,
        string EndDate
        );
}
