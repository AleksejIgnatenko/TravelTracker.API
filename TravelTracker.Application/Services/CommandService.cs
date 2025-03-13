using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.CommandModels;
using TravelTracker.Core.Abstractions;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Drawing;

namespace TravelTracker.Application.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IValidationService _validationService;

        public CommandService(ICommandRepository commandRepository, IValidationService validationService)
        {
            _commandRepository = commandRepository;
            _validationService = validationService;
        }

        public async Task CreateCommandAsync(string title, string description, string dateIssued)
        {
            var command = new CommandEntity
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                DateIssued = dateIssued
            };

            var validationErrors = _validationService.Validation(command);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _commandRepository.CreateAsync(command);
        }

        public async Task<IEnumerable<CommandEntity>> GetAllCommandsAsync()
        {
            return await _commandRepository.GetAllAsync();
        }

        public async Task<MemoryStream> ExportCommandsToExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var commands = await _commandRepository.GetAllAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Приказы");

                worksheet.Cells[1, 1].Value = "Идентификатор";
                worksheet.Cells[1, 2].Value = "Название команды";
                worksheet.Cells[1, 3].Value = "Описание";
                worksheet.Cells[1, 4].Value = "Дата выдачи";

                int row = 2;
                foreach (var command in commands)
                {
                    worksheet.Cells[row, 1].Value = command.Id;
                    worksheet.Cells[row, 2].Value = command.Title;
                    worksheet.Cells[row, 3].Value = command.Description;
                    worksheet.Cells[row, 4].Value = command.DateIssued;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                stream.Position = 0;
                return stream;
            }
        }

        public async Task<MemoryStream> ExportDateQuantityChartToExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var commands = await _commandRepository.GetAllAsync();

            var dateCounts = commands
                .GroupBy(c => c.DateIssued)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Приказы");

                worksheet.Cells[1, 1].Value = "Дата";
                worksheet.Cells[1, 2].Value = "Количество приказов";

                int row = 2;
                foreach (var dateCount in dateCounts)
                {
                    worksheet.Cells[row, 1].Value = dateCount.Date;
                    worksheet.Cells[row, 2].Value = dateCount.Count;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var chart = worksheet.Drawings.AddChart("LineChart", eChartType.Line);
                chart.Title.Text = "Количество приказов по датам";
                chart.SetPosition(row + 2, 0, 0, 0); 
                chart.SetSize(600, 400); 

                var series = chart.Series.Add(worksheet.Cells[2, 2, row - 1, 2], worksheet.Cells[2, 1, row - 1, 1]);

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                stream.Position = 0;
                return stream;
            }
        }

        public async Task UpdateCommandAsync(Guid id, string title, string description, string dateIssued)
        {
            var command = new CommandEntity
            {
                Id = id,
                Title = title,
                Description = description,
                DateIssued = dateIssued
            };

            var validationErrors = _validationService.Validation(command);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _commandRepository.UpdateAsync(command);
        }

        public async Task DeleteCommandAsync(Guid id)
        {
            await _commandRepository.DeleteAsync(await _commandRepository.GetByIdAsync(id));
        }
    }
}
