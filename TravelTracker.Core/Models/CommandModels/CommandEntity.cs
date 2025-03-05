using TravelTracker.Core.Models.BusinessTripModels;

namespace TravelTracker.Core.Models.CommandModels
{
    public class CommandEntity : BaseEntity
    {
        public IEnumerable<TripCertificateEntity> BusinessTrips { get; set; } = new List<TripCertificateEntity>();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;  
        public string DateIssued { get; set; } = string.Empty; 
    }
}