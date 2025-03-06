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

        public async Task<List<TripCertificateEntity>> GetByCityIdAsync(Guid cityId)
        {
            return await _context.TripCertificates
                .AsNoTracking()
                .Where(t => t.City.Id.Equals(cityId))
                .Include(t => t.Employee)
                .Include(t => t.Command)
                .Include(t => t.City)
                .ToListAsync();
        }

        public async Task<List<TripCertificateEntity>> GetByCommandIdAsync(Guid commandId)
        {
            return await _context.TripCertificates
                .AsNoTracking()
                .Where(t => t.Command.Id.Equals(commandId))
                .Include(t => t.Employee)
                .Include(t => t.Command)
                .Include(t => t.City)
                .ToListAsync();
        }

        public async Task<List<TripCertificateEntity>> GetByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.TripCertificates
                .AsNoTracking()
                .Where(t => t.Employee.Id.Equals(employeeId))
                .Include(t => t.Employee)
                .Include(t => t.Command)
                .Include(t => t.City)
                .ToListAsync();
        }
    }
}
