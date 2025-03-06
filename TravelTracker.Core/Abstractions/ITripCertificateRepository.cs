using TravelTracker.Core.Models.BusinessTripModels;

namespace TravelTracker.Core.Abstractions
{
    public interface ITripCertificateRepository : IRepositoryBase<TripCertificateEntity>
    {
        Task<List<TripCertificateEntity>> GetByCityIdAsync(Guid cityId);
        Task<List<TripCertificateEntity>> GetByCommandIdAsync(Guid commandId);
        Task<List<TripCertificateEntity>> GetByEmployeeIdAsync(Guid employeeId);
    }
}