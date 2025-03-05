namespace TravelTracker.Core.Models.TripExpenseModels
{
    public record TripExpenseRequest(
        Guid AdvanceReportId,
        Guid TripExpenseTypeId,
        decimal Amount,
        string Date,
        string Description
        );
}
