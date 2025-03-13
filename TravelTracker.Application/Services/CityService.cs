using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.CityModels;
using TravelTracker.Core.Abstractions;
using OfficeOpenXml;

namespace TravelTracker.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IValidationService _validationService;

        public CityService(ICityRepository cityRepository, IValidationService validationService)
        {
            _cityRepository = cityRepository;
            _validationService = validationService;
        }

        public async Task CreateCityAsync(string country, string name)
        {
            var city = new CityEntity
            {
                Id = Guid.NewGuid(),
                Country = country,
                Name = name,
            };

            var validationErrors = _validationService.Validation(city);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _cityRepository.CreateAsync(city);
        }

        public async Task<IEnumerable<CityEntity>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<MemoryStream> ExportCitiesToExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var cities = await _cityRepository.GetAllAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Города");

                worksheet.Cells[1, 1].Value = "Идентификатор";
                worksheet.Cells[1, 2].Value = "Название города";
                worksheet.Cells[1, 3].Value = "Страна";

                int row = 2;
                foreach (var city in cities)
                {
                    worksheet.Cells[row, 1].Value = city.Id;
                    worksheet.Cells[row, 2].Value = city.Name;
                    worksheet.Cells[row, 3].Value = city.Country;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                stream.Position = 0;
                return stream;
            }
        }

        public async Task UpdateCityAsync(Guid id, string country, string name)
        {
            var city = new CityEntity
            {
                Id = id,
                Country = country,
                Name = name,
            };

            var validationErrors = _validationService.Validation(city);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteCityAsync(Guid id)
        {
            await _cityRepository.DeleteAsync(await _cityRepository.GetByIdAsync(id));
        }
    }
}
