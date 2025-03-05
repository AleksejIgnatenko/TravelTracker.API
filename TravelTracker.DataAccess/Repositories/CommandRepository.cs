using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.CommandModels;
using TravelTracker.DataAccess.Context;

namespace TravelTracker.DataAccess.Repositories
{
    public class CommandRepository : RepositoryBase<CommandEntity>, ICommandRepository
    {
        public CommandRepository(TravelTrackerDbContext context) : base(context)
        {
        }
    }
}
