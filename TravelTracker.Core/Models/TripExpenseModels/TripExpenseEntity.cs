using TravelTracker.Core.Models.AdvanceReportModels;
using TravelTracker.Core.Models.TravelExpenseTypeModels;

namespace TravelTracker.Core.Models.TripExpenseModels
{
    public class TripExpenseEntity : BaseEntity
    {
        public AdvanceReportEntity AdvanceReport { get; set; } = new AdvanceReportEntity();
        public TripExpenseTypeEntity TripExpenseType { get; set; } = new TripExpenseTypeEntity();
        public decimal Amount { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}