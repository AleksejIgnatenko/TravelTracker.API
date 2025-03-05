using TravelTracker.Core.Models.BusinessTripModels;

namespace TravelTracker.Core.Abstractions
{
    public interface ITripCertificateService
    {
        Task CreateTripCertificateAsync(Guid employeeId, Guid commandId, Guid cityId, string startDate, string endDate);
        Task DeleteTripCertificateAsync(Guid id);
        Task<IEnumerable<TripCertificateEntity>> GetAllTripCertificatesAsync();
        Task UpdateTripCertificateAsync(Guid id, Guid employeeId, Guid commandId, Guid cityId, string startDate, string endDate);
    }
}