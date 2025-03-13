using TravelTracker.Core.Models.CommandModels;

namespace TravelTracker.Core.Abstractions
{
    public interface ICommandService
    {
        Task CreateCommandAsync(string title, string description, string dateIssued);
        Task DeleteCommandAsync(Guid id);
        Task<IEnumerable<CommandEntity>> GetAllCommandsAsync();
        Task<MemoryStream> ExportCommandsToExcelAsync();
        Task<MemoryStream> ExportDateQuantityChartToExcelAsync();
        Task UpdateCommandAsync(Guid id, string title, string description, string dateIssued);
    }
}