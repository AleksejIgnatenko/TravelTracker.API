using Microsoft.EntityFrameworkCore;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.TripExpenseModels;
using TravelTracker.DataAccess.Context;

namespace TravelTracker.DataAccess.Repositories
{
    public class TripExpenseRepository : RepositoryBase<TripExpenseEntity>, ITripExpenseRepository
    {
        public TripExpenseRepository(TravelTrackerDbContext context) : base(context)
        {
        }

        public override async Task<List<TripExpenseEntity>> GetAllAsync()
        {
            return await _context.TripExpenses
                .AsNoTracking()
                .Include(t => t.AdvanceReport)
                .Include(t => t.TripExpenseType)
                .ToListAsync();
        }

        public async Task<List<TripExpenseEntity>> GetByAdvanceReportIdAsync(Guid advanceReportId)
        {
            return await _context.TripExpenses
                .AsNoTracking()
                .Where(t => t.AdvanceReport.Id.Equals(advanceReportId))
                .Include(t => t.AdvanceReport)
                .Include(t => t.TripExpenseType)
                .ToListAsync();
        }

        public async Task<List<TripExpenseEntity>> GetByTripExpenseTypeIdAsync(Guid tripExpenseTypeId)
        {
            return await _context.TripExpenses
                .AsNoTracking()
                .Where(t => t.TripExpenseType.Id.Equals(tripExpenseTypeId))
                .Include(t => t.AdvanceReport)
                .Include(t => t.TripExpenseType)
                .ToListAsync();
        }
    }
}
