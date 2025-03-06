namespace TravelTracker.Core.Models.AdvanceReportModels
{
    public record AdvanceReportResponse(
        Guid Id,
        Guid TripCertificateId,
        string TripCertificateName,
        decimal TotalAmount,
        string DateOfDelivery
        );
}
