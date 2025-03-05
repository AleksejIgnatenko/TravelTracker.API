namespace TravelTracker.Core.Models.AdvanceReportModels
{
    public record AdvanceReportResponse(
        Guid Id,
        Guid TripCertificateId,
        decimal TotalAmount,
        string DateOfDelivery
        );
}
