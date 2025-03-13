using OfficeOpenXml;
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

            var tripExpenseValidationErrors = _validationService.Validation(tripExpense);
            if (tripExpenseValidationErrors.Count != 0)
            {
                throw new ValidationException(tripExpenseValidationErrors);
            }

            var advanceReportValidationErrors = _validationService.Validation(advanceReport);
            if (advanceReportValidationErrors.Count != 0)
            {
                throw new ValidationException(advanceReportValidationErrors);
            }

            await _tripExpenseRepository.CreateAsync(tripExpense);
            await _advanceReportRepository.UpdateAsync(advanceReport);
        }

        public async Task<IEnumerable<TripExpenseEntity>> GetAllTripExpensesAsync()
        {
            return await _tripExpenseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TripExpenseEntity>> GetTripExpenseByAdvanceReportIdAsync(Guid advanceReportId)
        {
            return await _tripExpenseRepository.GetByAdvanceReportIdAsync(advanceReportId);
        }

        public async Task<IEnumerable<TripExpenseEntity>> GetTripExpenseByTripExpenseTypeIdAsync(Guid tripExpenseTypeId)
        {
            return await _tripExpenseRepository.GetByTripExpenseTypeIdAsync(tripExpenseTypeId);
        }

        public async Task<MemoryStream> ExportTripExpensesToExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var tripExpenses = await _tripExpenseRepository.GetAllAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Командировочные расходы");

                worksheet.Cells[1, 1].Value = "Идентификатор";
                worksheet.Cells[1, 2].Value = "Сумма";
                worksheet.Cells[1, 3].Value = "Дата";
                worksheet.Cells[1, 4].Value = "Описание";

                int row = 2;
                foreach (var tripExpense in tripExpenses)
                {
                    worksheet.Cells[row, 1].Value = tripExpense.Id;
                    worksheet.Cells[row, 2].Value = tripExpense.Amount;
                    worksheet.Cells[row, 3].Value = tripExpense.Date;
                    worksheet.Cells[row, 4].Value = tripExpense.Description;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                stream.Position = 0;
                return stream;
            }
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

            var tripExpenseValidationErrors = _validationService.Validation(tripExpense);
            if (tripExpenseValidationErrors.Count != 0)
            {
                throw new ValidationException(tripExpenseValidationErrors);
            }

            var advanceReportValidationErrors = _validationService.Validation(advanceReport);
            if (advanceReportValidationErrors.Count != 0)
            {
                throw new ValidationException(advanceReportValidationErrors);
            }

            await _tripExpenseRepository.UpdateAsync(tripExpense);
            await _advanceReportRepository.UpdateAsync(advanceReport);
        }

        public async Task DeleteTripExpenseAsync(Guid id)
        {
            await _tripExpenseRepository.DeleteAsync(await _tripExpenseRepository.GetByIdAsync(id));
        }
    }
}
