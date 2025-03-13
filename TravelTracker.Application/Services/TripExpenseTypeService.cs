using OfficeOpenXml;
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

        public async Task<MemoryStream> ExportTripExpenseTypesToExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var tripExpenseTypes = await _tripExpenseTypeRepository.GetAllAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Виды командировочных расходов");

                worksheet.Cells[1, 1].Value = "Идентификатор";
                worksheet.Cells[1, 2].Value = "Название";
                worksheet.Cells[1, 3].Value = "Норма";

                int row = 2;
                foreach (var tripExpenseType in tripExpenseTypes)
                {
                    worksheet.Cells[row, 1].Value = tripExpenseType.Id;
                    worksheet.Cells[row, 2].Value = tripExpenseType.Name;
                    worksheet.Cells[row, 3].Value = tripExpenseType.Standard;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                stream.Position = 0;
                return stream;
            }
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
