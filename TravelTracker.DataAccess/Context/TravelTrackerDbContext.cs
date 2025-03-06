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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    var cities = new List<CityEntity>
        //    {
        //        new CityEntity { Id = Guid.NewGuid(), Country = "America", Name = "New York" },
        //        new CityEntity { Id = Guid.NewGuid(), Country = "America", Name = "Los Angeles" },
        //        new CityEntity { Id = Guid.NewGuid(), Country = "America", Name = "Chicago" },
        //    };
        //    modelBuilder.Entity<CityEntity>().HasData(cities);

        //    var employees = new List<EmployeeEntity>
        //    {
        //        new EmployeeEntity { Id = Guid.NewGuid(), FirstName = "Иванов", LastName = "Иван", MiddleName = "Иванович", Position = "Продавец", Department = "Sales" },
        //        new EmployeeEntity { Id = Guid.NewGuid(), FirstName = "Петров", LastName = "Петр", MiddleName = "Петрович", Position = "Программист", Department = "IT" },
        //        new EmployeeEntity { Id = Guid.NewGuid(), FirstName = "Сидоров", LastName = "Семен", MiddleName = "Семенович", Position = "Логист", Department = "Logic" },
        //    };
        //    modelBuilder.Entity<EmployeeEntity>().HasData(employees);

        //    var commands = new List<CommandEntity>
        //    {
        //        new CommandEntity { Id = Guid.NewGuid(), Title = "Title", Description = "Description", DateIssued = "0001-01-01" },
        //        new CommandEntity { Id = Guid.NewGuid(), Title = "Title1", Description = "Description1", DateIssued = "0001-01-01" },
        //        new CommandEntity { Id = Guid.NewGuid(), Title = "Title2", Description = "Description2", DateIssued = "0001-01-01" },
        //    };
        //    modelBuilder.Entity<CommandEntity>().HasData(commands);

        //    var tripCertificates = new List<TripCertificateEntity>
        //    {
        //        new TripCertificateEntity { Id = Guid.NewGuid(), Employee = employees[0], Command = commands[0], City = cities[0], Name = "Name", StartDate = "0000-00-00", EndDate = "0000-00-00" },
        //        new TripCertificateEntity { Id = Guid.NewGuid(), Employee = employees[1], Command = commands[1], City = cities[1], Name = "Name1", StartDate = "0000-00-00", EndDate = "0000-00-00" },
        //        new TripCertificateEntity { Id = Guid.NewGuid(), Employee = employees[2], Command = commands[2], City = cities[2], Name = "Name2", StartDate = "0000-00-00", EndDate = "0000-00-00" },
        //    };
        //    modelBuilder.Entity<TripCertificateEntity>().HasData(tripCertificates);

        //    var advanceReports = new List<AdvanceReportEntity>
        //    {
        //        new AdvanceReportEntity { Id = Guid.NewGuid(), TripCertificate = tripCertificates[0], DateOfDelivery = "0000-00-00" },
        //        new AdvanceReportEntity { Id = Guid.NewGuid(), TripCertificate = tripCertificates[1], DateOfDelivery = "0000-00-00" },
        //        new AdvanceReportEntity { Id = Guid.NewGuid(), TripCertificate = tripCertificates[2], DateOfDelivery = "0000-00-00" },
        //    };
        //    modelBuilder.Entity<AdvanceReportEntity>().HasData(advanceReports);

        //    var tripExpenseTypes = new List<TripExpenseTypeEntity>
        //    {
        //        new TripExpenseTypeEntity { Id = Guid.NewGuid(), Name = "Food", Standard = 100},
        //        new TripExpenseTypeEntity { Id = Guid.NewGuid(), Name = "Transit", Standard = 100},
        //        new TripExpenseTypeEntity { Id = Guid.NewGuid(), Name = "Transit", Standard = 100},
        //    };
        //    modelBuilder.Entity<TripExpenseTypeEntity>().HasData(tripExpenseTypes);

        //    var tripExpenses = new List<TripExpenseEntity>
        //    {
        //        new TripExpenseEntity { Id = Guid.NewGuid(), AdvanceReport = advanceReports[0], TripExpenseType = tripExpenseTypes[0], Amount = 0, Date = "0000-00-00", Description = "Description"},
        //        new TripExpenseEntity { Id = Guid.NewGuid(), AdvanceReport = advanceReports[1], TripExpenseType = tripExpenseTypes[1], Amount = 0, Date = "0000-00-00", Description = "Description1"},
        //        new TripExpenseEntity { Id = Guid.NewGuid(), AdvanceReport = advanceReports[2], TripExpenseType = tripExpenseTypes[2], Amount = 0, Date = "0000-00-00", Description = "Description2"},
        //    };
        //    modelBuilder.Entity<TripExpenseEntity>().HasData(tripExpenses);
        //}
    }
}
