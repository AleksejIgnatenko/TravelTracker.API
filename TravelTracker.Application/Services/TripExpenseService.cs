using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.Application.Services
{
    public class TripExpenseService : ITripExpenseService
    {
        private readonly ITripExpenseRepository _tripExpenseRepository;
        private readonly IAdvanceReportRepository _advanceReportRepository;
        private readonly ITripExpenseTypeRepository _tripExpenseTypeRepository;
        private readonly IValidationService _validationService;

        public TripExpenseService(ITripExpenseRepository tripExpenseRepository, IAdvanceReportRepository advanceReportRepository, ITripExpenseTypeRepository tripExpenseTypeRepository, IValidationService validationService)
        {
            _tripExpenseRepository = tripExpenseRepository;
            _advanceReportRepository = advanceReportRepository;
            _tripExpenseTypeRepository = tripExpenseTypeRepository;
            _validationService = validationService;
        }

        public async Task CreateTripExpenseAsync(Guid advanceReportEntityId, Guid tripExpenseTypeId, decimal amount, string date, string description)
        {
            var advanceReport = await _advanceReportRepository.GetByIdAsync(advanceReportEntityId);
            var tripExpenseType = await _tripExpenseTypeRepository.GetByIdAsync(tripExpenseTypeId);

            var tripExpense = new TripExpenseEntity
            {
                Id = Guid.NewGuid(),
                AdvanceReport = advanceReport,
                TripExpenseType = tripExpenseType,
                Amount = amount,
                Date = date,
                Description = description,
            };

            var validationErrors = _validationService.Validation(tripExpense);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _tripExpenseRepository.CreateAsync(tripExpense);
        }

        public async Task<IEnumerable<TripExpenseEntity>> GetAllTripExpensesAsync()
        {
            return await _tripExpenseRepository.GetAllAsync();
        }

        public async Task UpdateTripExpenseAsync(Guid id, Guid advanceReportEntityId, Guid tripExpenseTypeId, decimal amount, string date, string description)
        {
            var advanceReport = await _advanceReportRepository.GetByIdAsync(advanceReportEntityId);
            var tripExpenseType = await _tripExpenseTypeRepository.GetByIdAsync(tripExpenseTypeId);

            var tripExpense = new TripExpenseEntity
            {
                Id = id,
                AdvanceReport = advanceReport,
                TripExpenseType = tripExpenseType,
                Amount = amount,
                Date = date,
                Description = description,
            };

            var validationErrors = _validationService.Validation(tripExpense);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _tripExpenseRepository.UpdateAsync(tripExpense);
        }

        public async Task DeleteTripExpenseAsync(Guid id)
        {
            await _tripExpenseRepository.DeleteAsync(await _tripExpenseRepository.GetByIdAsync(id));
        }
    }
}
