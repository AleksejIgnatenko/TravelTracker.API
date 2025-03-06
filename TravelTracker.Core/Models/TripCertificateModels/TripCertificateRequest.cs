namespace TravelTracker.Core.Models.TripCertificateModels
{
    public record TripCertificateRequest(
        string Name,
        Guid EmployeeId,
        Guid CommandId,
        Guid CityId,
        string StartDate,
        string EndDate
        );
}
