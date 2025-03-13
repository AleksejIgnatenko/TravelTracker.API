using TravelTracker.Core.Models.EmployeeModels;

namespace TravelTracker.Core.Abstractions
{
    public interface IEmployeeService
    {
        Task CreateEmployeeAsync(string firstName, string lastName, string middleName, string position, string department);
        Task DeleteEmployeeAsync(Guid id);
        Task<IEnumerable<EmployeeEntity>> GetAllEmployeesAsync();
        Task<MemoryStream> ExportEmployeesToExcelAsync();
        Task UpdateEmployeeAsync(Guid id, string firstName, string lastName, string middleName, string position, string department);
    }
}