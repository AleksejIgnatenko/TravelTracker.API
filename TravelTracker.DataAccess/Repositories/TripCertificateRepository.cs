using Microsoft.EntityFrameworkCore;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.BusinessTripModels;
using TravelTracker.DataAccess.Context;

namespace TravelTracker.DataAccess.Repositories
{
    public class TripCertificateRepository : RepositoryBase<TripCertificateEntity>, ITripCertificateRepository
    {
        public TripCertificateRepository(TravelTrackerDbContext context) : base(context)
        {
        }

        public override async Task<List<TripCertificateEntity>> GetAllAsync()
        {
            return await _context.TripCertificates
                .AsNoTracking()
                .Include(t => t.Employee)
                .Include(t => t.Command)
                .Include(t => t.City)
                .ToListAsync();
        }
    }
}
