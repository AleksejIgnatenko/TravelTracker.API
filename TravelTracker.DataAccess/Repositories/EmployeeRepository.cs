using Microsoft.EntityFrameworkCore;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.EmployeeModels;
using TravelTracker.DataAccess.Context;

namespace TravelTracker.DataAccess.Repositories
{
    public class EmployeeRepository : RepositoryBase<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(TravelTrackerDbContext context) : base(context)
        {
        }

        public override async Task<List<EmployeeEntity>> GetAllAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(e => e.BusinessTrips)
                .ToListAsync();
        }
    }
}
