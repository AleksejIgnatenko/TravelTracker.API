using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.Core.Abstractions
{
    public interface ITripExpenseRepository : IRepositoryBase<TripExpenseEntity>
    {
        Task<List<TripExpenseEntity>> GetByAdvanceReportIdAsync(Guid advanceReportId);
        Task<List<TripExpenseEntity>> GetByTripExpenseTypeIdAsync(Guid tripExpenseTypeId);
    }
}