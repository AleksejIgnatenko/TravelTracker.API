namespace TravelTracker.Core.Models.CommandModels
{
    public record CommandResponse(
        Guid Id,
        string Title,
        string Description,
        string DateIssued
        );
}
