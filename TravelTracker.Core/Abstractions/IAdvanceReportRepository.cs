using TravelTracker.Core.Models.AdvanceReportModels;

namespace TravelTracker.Core.Abstractions
{
    public interface IAdvanceReportRepository : IRepositoryBase<AdvanceReportEntity>
    {
        Task<List<AdvanceReportEntity>> GetByTripCertificateIdAsync(Guid tripCertificateId);
    }
}