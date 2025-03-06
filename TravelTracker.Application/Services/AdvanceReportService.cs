using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.AdvanceReportModels;

namespace TravelTracker.Application.Services
{
    public class AdvanceReportService : IAdvanceReportService
    {
        private readonly IAdvanceReportRepository _advanceReportRepository;
        private readonly ITripCertificateRepository _tripCertificateRepository;
        private readonly IValidationService _validationService;

        public AdvanceReportService(IAdvanceReportRepository advanceReportRepository, ITripCertificateRepository tripCertificateRepository, IValidationService validationService)
        {
            _advanceReportRepository = advanceReportRepository;
            _tripCertificateRepository = tripCertificateRepository;
            _validationService = validationService;
        }

        public async Task CreateAdvanceReportAsync(Guid tripCertificateId, string dateOfDelivery)
        {
            var tripCertificate = await _tripCertificateRepository.GetByIdAsync(tripCertificateId);

            var advanceReport = new AdvanceReportEntity
            {
                Id = Guid.NewGuid(),
                TripCertificate = tripCertificate,
                DateOfDelivery = dateOfDelivery,
            };

            var validationErrors = _validationService.Validation(advanceReport);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _advanceReportRepository.CreateAsync(advanceReport);
        }

        public async Task<IEnumerable<AdvanceReportEntity>> GetAllAdvanceReportsAsync()
        {
            return await _advanceReportRepository.GetAllAsync();
        }

        public async Task<IEnumerable<AdvanceReportEntity>> GetAdvanceReportByTripCertificateIdAsync(Guid tripCertificateId)
        {
            return await _advanceReportRepository.GetByTripCertificateIdAsync(tripCertificateId);
        }

        public async Task UpdateAdvanceReportAsync(Guid id, Guid tripCertificateId, string dateOfDelivery)
        {
            var advanceReport = await _advanceReportRepository.GetByIdAsync(id);
            var tripCertificate = await _tripCertificateRepository.GetByIdAsync(tripCertificateId);

            advanceReport.TripCertificate = tripCertificate;
            advanceReport.DateOfDelivery = dateOfDelivery;

            var validationErrors = _validationService.Validation(advanceReport);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _advanceReportRepository.UpdateAsync(advanceReport);
        }

        public async Task DeleteAdvanceReportAsync(Guid id)
        {
            await _advanceReportRepository.DeleteAsync(await _advanceReportRepository.GetByIdAsync(id));
        }
    }
}
