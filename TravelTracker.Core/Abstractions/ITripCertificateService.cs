using TravelTracker.Core.Models.BusinessTripModels;

namespace TravelTracker.Core.Abstractions
{
    public interface ITripCertificateService
    {
        Task CreateTripCertificateAsync(string name, Guid employeeId, Guid commandId, Guid cityId, string startDate, string endDate);
        Task DeleteTripCertificateAsync(Guid id);
        Task<IEnumerable<TripCertificateEntity>> GetAllTripCertificatesAsync();
        Task<IEnumerable<TripCertificateEntity>> GetTripCertificateByCityIdAsync(Guid cityId);
        Task<IEnumerable<TripCertificateEntity>> GetTripCertificateByCommandIdAsync(Guid commandId);
        Task<IEnumerable<TripCertificateEntity>> GetTripCertificateByEmployeeIdAsync(Guid employeeId);
        Task<MemoryStream> ExportTripCertificatesToExcelAsync();
        Task<MemoryStream> GenerateTripCertificateToWordAsync(Guid id);
        Task UpdateTripCertificateAsync(Guid id, string name, Guid employeeId, Guid commandId, Guid cityId, string startDate, string endDate);
    }
}