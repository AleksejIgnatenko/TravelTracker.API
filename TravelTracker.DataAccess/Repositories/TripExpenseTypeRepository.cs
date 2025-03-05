using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.TravelExpenseTypeModels;
using TravelTracker.DataAccess.Context;

namespace TravelTracker.DataAccess.Repositories
{
    public class TripExpenseTypeRepository : RepositoryBase<TripExpenseTypeEntity>, ITripExpenseTypeRepository
    {
        public TripExpenseTypeRepository(TravelTrackerDbContext context) : base(context)
        {
        }
    }
}
