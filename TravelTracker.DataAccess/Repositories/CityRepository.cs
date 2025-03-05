using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.CityModels;
using TravelTracker.DataAccess.Context;

namespace TravelTracker.DataAccess.Repositories
{
    public class CityRepository : RepositoryBase<CityEntity>, ICityRepository
    {
        public CityRepository(TravelTrackerDbContext context) : base(context)
        {
        }
    }
}
