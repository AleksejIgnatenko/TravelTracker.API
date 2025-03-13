using Microsoft.AspNetCore.Mvc;
using TravelTracker.Application.Services;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.AdvanceReportModels;
using TravelTracker.Core.Models.TripCertificateModels;

namespace TravelTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripCertificateController : ControllerBase
    {
        private readonly ITripCertificateService _tripCertificateService;

        public TripCertificateController(ITripCertificateService tripCertificateService)
        {
            _tripCertificateService = tripCertificateService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTripCertificateAsync([FromBody] TripCertificateRequest tripCertificateRequest)
        {
            await _tripCertificateService.CreateTripCertificateAsync(tripCertificateRequest.Name, tripCertificateRequest.EmployeeId, tripCertificateRequest.CommandId, tripCertificateRequest.CityId, tripCertificateRequest.StartDate, tripCertificateRequest.EndDate);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTripCertificatesAsync()
        {
            var tripCertificates = await _tripCertificateService.GetAllTripCertificatesAsync();

            var response = tripCertificates.Select(t => new TripCertificateResponse(t.Id, t.Name, t.Employee.Id, 
                t.Employee.FirstName + " " + t.Employee.LastName + " " + t.Employee.MiddleName, t.Command.Id, 
                t.Command.Title, t.City.Id, t.City.Name, t.StartDate, t.EndDate));

            return Ok(response);
        }

        [HttpGet("cityId={cityId:guid}")]
        public async Task<ActionResult> GetTripCertificateByCityIdAsync(Guid cityId)
        {
            var tripCertificates = await _tripCertificateService.GetTripCertificateByCityIdAsync(cityId);

            var response = tripCertificates.Select(t => new TripCertificateResponse(t.Id, t.Name, t.Employee.Id,
                t.Employee.FirstName + " " + t.Employee.LastName + " " + t.Employee.MiddleName, t.Command.Id,
                t.Command.Title, t.City.Id, t.City.Name, t.StartDate, t.EndDate));

            return Ok(response);
        }

        [HttpGet("commandId={commandId:guid}")]
        public async Task<ActionResult> GetTripCertificateByCommandIdAsync(Guid commandId)
        {
            var tripCertificates = await _tripCertificateService.GetTripCertificateByCommandIdAsync(commandId);

            var response = tripCertificates.Select(t => new TripCertificateResponse(t.Id, t.Name, t.Employee.Id,
                t.Employee.FirstName + " " + t.Employee.LastName + " " + t.Employee.MiddleName, t.Command.Id,
                t.Command.Title, t.City.Id, t.City.Name, t.StartDate, t.EndDate));

            return Ok(response);
        }

        [HttpGet("employeeId={employeeId:guid}")]
        public async Task<ActionResult> GetTripCertificateByEmployeeIdAsync(Guid employeeId)
        {
            var tripCertificates = await _tripCertificateService.GetTripCertificateByEmployeeIdAsync(employeeId);

            var response = tripCertificates.Select(t => new TripCertificateResponse(t.Id, t.Name, t.Employee.Id,
                t.Employee.FirstName + " " + t.Employee.LastName + " " + t.Employee.MiddleName, t.Command.Id,
                t.Command.Title, t.City.Id, t.City.Name, t.StartDate, t.EndDate));

            return Ok(response);
        }

        [HttpGet("export-to-excel")]
        public async Task<ActionResult> ExportToExcelAsync()
        {
            var stream = await _tripCertificateService.ExportTripCertificatesToExcelAsync();
            var fileName = "items.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTripCertificateAsync(Guid id, [FromBody] TripCertificateRequest tripCertificateRequest)
        {
            await _tripCertificateService.UpdateTripCertificateAsync(id, tripCertificateRequest.Name, tripCertificateRequest.EmployeeId, tripCertificateRequest.CommandId, tripCertificateRequest.CityId, tripCertificateRequest.StartDate, tripCertificateRequest.EndDate);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTripCertificateAsync(Guid id)
        {
            await _tripCertificateService.DeleteTripCertificateAsync(id);

            return Ok();
        }
    }
}
