namespace TravelTracker.Core.Models.CommandModels
{
    public record CommandRequest(
        string Title,
        string Description,
        string DateIssued
        );
}
