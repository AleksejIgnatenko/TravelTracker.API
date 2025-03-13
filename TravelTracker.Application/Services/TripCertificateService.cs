using OfficeOpenXml;
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

        public async Task CreateTripCertificateAsync(string name, Guid employeeId, Guid commandId, Guid cityId, string startDate, string endDate)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            var command = await _commandRepository.GetByIdAsync(commandId);
            var city = await _cityRepository.GetByIdAsync(cityId);

            var tripCertificate = new TripCertificateEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
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

        public async Task<IEnumerable<TripCertificateEntity>> GetTripCertificateByCityIdAsync(Guid cityId)
        {
            return await _tripCertificateRepository.GetByCityIdAsync(cityId);
        }

        public async Task<IEnumerable<TripCertificateEntity>> GetTripCertificateByCommandIdAsync(Guid commandId)
        {
            return await _tripCertificateRepository.GetByCommandIdAsync(commandId);
        }

        public async Task<IEnumerable<TripCertificateEntity>> GetTripCertificateByEmployeeIdAsync(Guid employeeId)
        {
            return await _tripCertificateRepository.GetByEmployeeIdAsync(employeeId);
        }

        public async Task<MemoryStream> ExportTripCertificatesToExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var tripCertificates = await _tripCertificateRepository.GetAllAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Командировочные удостоверения");

                worksheet.Cells[1, 1].Value = "Идентификатор";
                worksheet.Cells[1, 2].Value = "Название";
                worksheet.Cells[1, 3].Value = "Дата начала";
                worksheet.Cells[1, 4].Value = "Дата окончания";

                int row = 2;
                foreach (var tripCertificate in tripCertificates)
                {
                    worksheet.Cells[row, 1].Value = tripCertificate.Id;
                    worksheet.Cells[row, 2].Value = tripCertificate.Name;
                    worksheet.Cells[row, 3].Value = tripCertificate.StartDate;
                    worksheet.Cells[row, 4].Value = tripCertificate.EndDate;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                stream.Position = 0;
                return stream;
            }
        }

        public async Task UpdateTripCertificateAsync(Guid id, string name, Guid employeeId, Guid commandId, Guid cityId, string startDate, string endDate)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            var command = await _commandRepository.GetByIdAsync(commandId);
            var city = await _cityRepository.GetByIdAsync(cityId);

            var tripCertificate = new TripCertificateEntity
            {
                Id = id,
                Name = name,
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
