using TravelTracker.Core.Models.AdvanceReportModels;

namespace TravelTracker.Core.Abstractions
{
    public interface IAdvanceReportService
    {
        Task CreateAdvanceReportAsync(Guid tripCertificateId, string dateOfDelivery);
        Task DeleteAdvanceReportAsync(Guid id);
        Task<IEnumerable<AdvanceReportEntity>> GetAllAdvanceReportsAsync();
        Task<IEnumerable<AdvanceReportEntity>> GetAdvanceReportByTripCertificateIdAsync(Guid tripCertificateId);
        Task UpdateAdvanceReportAsync(Guid id, Guid tripCertificateId, string dateOfDelivery);
    }
}