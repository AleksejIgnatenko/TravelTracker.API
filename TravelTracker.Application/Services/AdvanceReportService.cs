using OfficeOpenXml;
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

        public async Task<MemoryStream> ExportAdvanceReportsToExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var advanceReports = await _advanceReportRepository.GetAllAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Авансовый отчет");

                worksheet.Cells[1, 1].Value = "Идентификатор отчета";
                worksheet.Cells[1, 2].Value = "Дата сдачи";
                worksheet.Cells[1, 3].Value = "Название";
                worksheet.Cells[1, 4].Value = "Дата начала";
                worksheet.Cells[1, 5].Value = "Дата окончания";

                int row = 2;
                foreach (var report in advanceReports)
                {
                    worksheet.Cells[row, 1].Value = report.Id;
                    worksheet.Cells[row, 2].Value = report.DateOfDelivery;
                    worksheet.Cells[row, 3].Value = report.TripCertificate.Name;
                    worksheet.Cells[row, 4].Value = report.TripCertificate.StartDate;
                    worksheet.Cells[row, 5].Value = report.TripCertificate.EndDate;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                stream.Position = 0;
                return stream;
            }
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
