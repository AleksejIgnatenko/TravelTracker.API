using TravelTracker.Core.Models.CityModels;
using TravelTracker.Core.Models.CommandModels;
using TravelTracker.Core.Models.EmployeeModels;

namespace TravelTracker.Core.Models.BusinessTripModels
{
    public class TripCertificateEntity : BaseEntity
    {
        //public AdvanceReportEntity AdvanceReport { get; set; } = new AdvanceReportEntity();
        public EmployeeEntity Employee { get; set; } = new EmployeeEntity();
        public CommandEntity Command { get; set; } = new CommandEntity();
        public CityEntity City { get; set; } = new CityEntity();
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
    }
}
