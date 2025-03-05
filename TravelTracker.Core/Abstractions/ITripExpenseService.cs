using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.Core.Abstractions
{
    public interface ITripExpenseService
    {
        Task CreateTripExpenseAsync(Guid advanceReportEntityId, Guid tripExpenseTypeId, decimal amount, string date, string description);
        Task DeleteTripExpenseAsync(Guid id);
        Task<IEnumerable<TripExpenseEntity>> GetAllTripExpensesAsync();
        Task UpdateTripExpenseAsync(Guid id, Guid advanceReportEntityId, Guid tripExpenseTypeId, decimal amount, string date, string description);
    }
}