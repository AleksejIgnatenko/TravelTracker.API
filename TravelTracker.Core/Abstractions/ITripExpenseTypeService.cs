using TravelTracker.Core.Models.TravelExpenseTypeModels;

namespace TravelTracker.Core.Abstractions
{
    public interface ITripExpenseTypeService
    {
        Task CreateTripExpenseTypeAsync(string name, decimal standard);
        Task DeleteTripExpenseTypeAsync(Guid id);
        Task<IEnumerable<TripExpenseTypeEntity>> GetAllTripExpenseTypesAsync();
        Task<MemoryStream> ExportTripExpenseTypesToExcelAsync();
        Task UpdateTripExpenseTypeAsync(Guid id, string name, decimal standard);
    }
}