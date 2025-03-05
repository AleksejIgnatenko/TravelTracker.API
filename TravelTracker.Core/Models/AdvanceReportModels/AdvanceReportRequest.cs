namespace TravelTracker.Core.Models.AdvanceReportModels
{
    public record AdvanceReportRequest(
        Guid TripCertificateId,
        decimal TotalAmount,
        string DateOfDelivery
        );
}
