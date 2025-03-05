using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.Core.Models.TravelExpenseTypeModels
{
    public class TripExpenseTypeEntity : BaseEntity
    {
        public IEnumerable<TripExpenseEntity> TripExpenses { get; set; } = new List<TripExpenseEntity>();
        public string Name { get; set; } = string.Empty;
        public decimal Standard { get; set; }
    }
}