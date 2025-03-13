using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.EmployeeModels;
using TravelTracker.Core.Abstractions;
using OfficeOpenXml;

namespace TravelTracker.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidationService _validationService;

        public EmployeeService(IEmployeeRepository employeeRepository, IValidationService validationService)
        {
            _employeeRepository = employeeRepository;
            _validationService = validationService;
        }

        public async Task CreateEmployeeAsync(string firstName, string lastName, string middleName, string position, string department)
        {
            var employee = new EmployeeEntity
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Position = position,
                Department = department,
            };

            var validationErrors = _validationService.Validation(employee);
            if(validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _employeeRepository.CreateAsync(employee);
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<MemoryStream> ExportEmployeesToExcelAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var employees = await _employeeRepository.GetAllAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Сотрудники");

                worksheet.Cells[1, 1].Value = "Идентификатор";
                worksheet.Cells[1, 2].Value = "Имя";
                worksheet.Cells[1, 3].Value = "Фамилия";
                worksheet.Cells[1, 4].Value = "Отчество";
                worksheet.Cells[1, 5].Value = "Должность";
                worksheet.Cells[1, 6].Value = "Отдел";

                int row = 2;
                foreach (var employee in employees)
                {
                    worksheet.Cells[row, 1].Value = employee.Id;
                    worksheet.Cells[row, 2].Value = employee.FirstName;
                    worksheet.Cells[row, 3].Value = employee.LastName;
                    worksheet.Cells[row, 4].Value = employee.MiddleName;
                    worksheet.Cells[row, 5].Value = employee.Position;
                    worksheet.Cells[row, 6].Value = employee.Department;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                stream.Position = 0;
                return stream;
            }
        }

        public async Task UpdateEmployeeAsync(Guid id, string firstName, string lastName, string middleName, string position, string department)
        {
            var employee = new EmployeeEntity
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Position = position,
                Department = department,
            };

            var validationErrors = _validationService.Validation(employee);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(await _employeeRepository.GetByIdAsync(id));
        }
    }
}
