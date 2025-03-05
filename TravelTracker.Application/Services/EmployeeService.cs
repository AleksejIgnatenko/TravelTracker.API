using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.EmployeeModels;
using TravelTracker.Core.Abstractions;

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
