using Microsoft.EntityFrameworkCore;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.AdvanceReportModels;
using TravelTracker.DataAccess.Context;

namespace TravelTracker.DataAccess.Repositories
{
    public class AdvanceReportRepository : RepositoryBase<AdvanceReportEntity>, IAdvanceReportRepository
    {
        public AdvanceReportRepository(TravelTrackerDbContext context) : base(context)
        {
        }

        public override async Task<List<AdvanceReportEntity>> GetAllAsync()
        {
            return await _context.AdvanceReports
                .AsNoTracking()
                .Include(a => a.TripCertificate)
                .Include(a => a.TripExpenses)
                .ToListAsync();
        }

        public async Task<List<AdvanceReportEntity>> GetByTripCertificateIdAsync(Guid tripCertificateId)
        {
            return await _context.AdvanceReports
                .AsNoTracking()
                .Where(a => a.TripCertificate.Id.Equals(tripCertificateId))
                .Include(a => a.TripCertificate)
                .Include(a => a.TripExpenses)
                .ToListAsync();
        }
    }
}
