using TravelTracker.Core.Models.AdvanceReportModels;

namespace TravelTracker.Core.Abstractions
{
    public interface IAdvanceReportService
    {
        Task CreateAdvanceReportAsync(Guid tripCertificateId, decimal totalAmount, string dateOfDelivery);
        Task DeleteAdvanceReportAsync(Guid id);
        Task<IEnumerable<AdvanceReportEntity>> GetAllAdvanceReportsAsync();
        Task UpdateAdvanceReportAsync(Guid id, Guid tripCertificateId, decimal totalAmount, string dateOfDelivery);
    }
}