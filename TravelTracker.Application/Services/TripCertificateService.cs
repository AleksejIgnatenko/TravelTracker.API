using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.BusinessTripModels;

namespace TravelTracker.Application.Services
{
    public class TripCertificateService : ITripCertificateService
    {
        private readonly ITripCertificateRepository _tripCertificateRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICommandRepository _commandRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IValidationService _validationService;

        public TripCertificateService(ITripCertificateRepository tripCertificateRepository, IEmployeeRepository employeeRepository, ICommandRepository commandRepository, ICityRepository cityRepository, IValidationService validationService)
        {
            _tripCertificateRepository = tripCertificateRepository;
            _employeeRepository = employeeRepository;
            _commandRepository = commandRepository;
            _cityRepository = cityRepository;
            _validationService = validationService;
        }

        public async Task CreateTripCertificateAsync(Guid employeeId, Guid commandId, Guid cityId, string startDate, string endDate)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            var command = await _commandRepository.GetByIdAsync(commandId);
            var city = await _cityRepository.GetByIdAsync(cityId);

            var tripCertificate = new TripCertificateEntity
            {
                Id = Guid.NewGuid(),
                Employee = employee,
                Command = command,
                City = city,
                StartDate = startDate,
                EndDate = endDate,
            };

            var validationErrors = _validationService.Validation(tripCertificate);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _tripCertificateRepository.CreateAsync(tripCertificate);
        }

        public async Task<IEnumerable<TripCertificateEntity>> GetAllTripCertificatesAsync()
        {
            return await _tripCertificateRepository.GetAllAsync();
        }

        public async Task UpdateTripCertificateAsync(Guid id, Guid employeeId, Guid commandId, Guid cityId, string startDate, string endDate)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            var command = await _commandRepository.GetByIdAsync(commandId);
            var city = await _cityRepository.GetByIdAsync(cityId);

            var tripCertificate = new TripCertificateEntity
            {
                Id = id,
                Employee = employee,
                Command = command,
                City = city,
                StartDate = startDate,
                EndDate = endDate,
            };

            var validationErrors = _validationService.Validation(tripCertificate);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _tripCertificateRepository.UpdateAsync(tripCertificate);
        }

        public async Task DeleteTripCertificateAsync(Guid id)
        {
            await _tripCertificateRepository.DeleteAsync(await _tripCertificateRepository.GetByIdAsync(id));
        }
    }
}
