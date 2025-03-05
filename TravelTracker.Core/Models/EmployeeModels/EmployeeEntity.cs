using TravelTracker.Core.Models.BusinessTripModels;

namespace TravelTracker.Core.Models.EmployeeModels
{
    public class EmployeeEntity : BaseEntity
    {
        public IEnumerable<TripCertificateEntity> BusinessTrips { get; set; } = new List<TripCertificateEntity>();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
}
