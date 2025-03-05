using Microsoft.AspNetCore.Mvc;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.EmployeeModels;

namespace TravelTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployeeAsync([FromBody] EmployeeRequest employeeRequest)
        {
            await _employeeService.CreateEmployeeAsync(employeeRequest.FirstName, employeeRequest.LastName,
                employeeRequest.MiddleName, employeeRequest.Position, employeeRequest.Department);

            return Ok();
         }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployeesAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            var response = employees.Select(e => new EmployeeResponse(e.Id, e.FirstName, e.LastName, e.MiddleName, e.Position, e.Department));

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateEmployeeAsync(Guid id, [FromBody] EmployeeRequest employeeRequest)
        {
            await _employeeService.UpdateEmployeeAsync(id, employeeRequest.FirstName, employeeRequest.LastName,
                employeeRequest.MiddleName, employeeRequest.Position, employeeRequest.Department);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);

            return Ok();
        }
    }
}
