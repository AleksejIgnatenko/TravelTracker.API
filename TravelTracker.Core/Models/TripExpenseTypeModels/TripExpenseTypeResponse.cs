namespace TravelTracker.Core.Models.TripExpenseTypeModels
{
    public record TripExpenseTypeResponse(
        Guid Id,
        string Name,
        decimal Standard
        );
}
