namespace TravelTracker.Core.Models.TripCertificateModels
{
    public record TripCertificateRequest(
        Guid EmployeeId,
        Guid CommandId,
        Guid CityId,
        string StartDate,
        string EndDate
        );
}
