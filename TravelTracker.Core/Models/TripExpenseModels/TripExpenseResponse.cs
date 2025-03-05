namespace TravelTracker.Core.Models.TripExpenseModels
{
    public record TripExpenseResponse(
        Guid Id,
        Guid AdvanceReportId,
        Guid TripExpenseTypeId,
        string TripExpenseTypeName,
        decimal Amount,
        string Date,
        string Description
        );
}
