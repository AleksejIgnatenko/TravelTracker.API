using Microsoft.EntityFrameworkCore;
using TravelTracker.Core.Models.AdvanceReportModels;
using TravelTracker.Core.Models.BusinessTripModels;
using TravelTracker.Core.Models.CityModels;
using TravelTracker.Core.Models.CommandModels;
using TravelTracker.Core.Models.EmployeeModels;
using TravelTracker.Core.Models.TravelExpenseTypeModels;
using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.DataAccess.Context
{
    public class TravelTrackerDbContext : DbContext
    {
        public DbSet<AdvanceReportEntity> AdvanceReports { get; set; }
        public DbSet<TripCertificateEntity> TripCertificates { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CommandEntity> Commands { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<TripExpenseTypeEntity> TripExpenseTypes { get; set; }
        public DbSet<TripExpenseEntity> TripExpenses { get; set; }

        public TravelTrackerDbContext(DbContextOptions<TravelTrackerDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
