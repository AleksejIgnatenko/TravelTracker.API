using TravelTracker.Core.Models.BusinessTripModels;

namespace TravelTracker.Core.Models.CityModels
{
    public class CityEntity : BaseEntity
    {
        public IEnumerable<TripCertificateEntity> BusinessTrips { get; set; }  = new List<TripCertificateEntity>();
        public string Country { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
