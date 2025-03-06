using TravelTracker.Core.Models.BusinessTripModels;
using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.Core.Models.AdvanceReportModels
{
    public class AdvanceReportEntity : BaseEntity
    {
        public TripCertificateEntity TripCertificate { get; set; } = new TripCertificateEntity();
        public IEnumerable<TripExpenseEntity> TripExpenses { get; set; } = new List<TripExpenseEntity>();
        public string DateOfDelivery { get; set; } = string.Empty;
    }
}
