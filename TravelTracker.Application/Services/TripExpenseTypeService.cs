using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.TravelExpenseTypeModels;

namespace TravelTracker.Application.Services
{
    public class TripExpenseTypeService : ITripExpenseTypeService
    {
        private readonly ITripExpenseTypeRepository _tripExpenseTypeRepository;
        private readonly IValidationService _validationService;

        public TripExpenseTypeService(ITripExpenseTypeRepository tripExpenseTypeRepository, IValidationService validationService)
        {
            _tripExpenseTypeRepository = tripExpenseTypeRepository;
            _validationService = validationService;
        }

        public async Task CreateTripExpenseTypeAsync(string name, decimal standard)
        {
            var tripCertificate = new TripExpenseTypeEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Standard = standard,
            };

            var validationErrors = _validationService.Validation(tripCertificate);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _tripExpenseTypeRepository.CreateAsync(tripCertificate);
        }

        public async Task<IEnumerable<TripExpenseTypeEntity>> GetAllTripExpenseTypesAsync()
        {
            return await _tripExpenseTypeRepository.GetAllAsync();
        }

        public async Task UpdateTripExpenseTypeAsync(Guid id, string name, decimal standard)
        {
            var tripCertificate = new TripExpenseTypeEntity
            {
                Id = id,
                Name = name,
                Standard = standard,
            };

            var validationErrors = _validationService.Validation(tripCertificate);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _tripExpenseTypeRepository.UpdateAsync(tripCertificate);
        }

        public async Task DeleteTripExpenseTypeAsync(Guid id)
        {
            await _tripExpenseTypeRepository.DeleteAsync(await _tripExpenseTypeRepository.GetByIdAsync(id));
        }
    }
}
