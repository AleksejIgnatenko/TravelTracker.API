using Microsoft.AspNetCore.Mvc;
using TravelTracker.Application.Services;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.TripExpenseModels;

namespace TravelTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripExpenseController : ControllerBase
    {
        private readonly ITripExpenseService _tripExpenseService;

        public TripExpenseController(ITripExpenseService tripExpenseService)
        {
            _tripExpenseService = tripExpenseService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdvanceReportAsync([FromBody] TripExpenseRequest tripExpenseRequest)
        {
            await  _tripExpenseService.CreateTripExpenseAsync(tripExpenseRequest.AdvanceReportId, tripExpenseRequest.TripExpenseTypeId, tripExpenseRequest.Amount, tripExpenseRequest.Date, tripExpenseRequest.Description);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAdvanceReportsAsync()
        {
            var advanceReports = await _tripExpenseService.GetAllTripExpensesAsync();

            var response = advanceReports.Select(t => new TripExpenseResponse(t.Id, t.AdvanceReport.Id, t.TripExpenseType.Id, t.TripExpenseType.Name, t.Amount, t.Date, t.Description));

            return Ok(response);
        }

        [HttpGet("advanceReportId={advanceReportId:guid}")]
        public async Task<ActionResult> GetTripExpenseByAdvanceReportIdAsync(Guid advanceReportId)
        {
            var advanceReports = await _tripExpenseService.GetTripExpenseByAdvanceReportIdAsync(advanceReportId);

            var response = advanceReports.Select(t => new TripExpenseResponse(t.Id, t.AdvanceReport.Id, t.TripExpenseType.Id, t.TripExpenseType.Name, t.Amount, t.Date, t.Description));

            return Ok(response);
        }

        [HttpGet("tripExpenseTypeId={tripExpenseTypeId:guid}")]
        public async Task<ActionResult> GetTripExpenseByTripExpenseTypeIdAsync(Guid tripExpenseTypeId)
        {
            var advanceReports = await _tripExpenseService.GetTripExpenseByTripExpenseTypeIdAsync(tripExpenseTypeId);

            var response = advanceReports.Select(t => new TripExpenseResponse(t.Id, t.AdvanceReport.Id, t.TripExpenseType.Id, t.TripExpenseType.Name, t.Amount, t.Date, t.Description));

            return Ok(response);
        }

        [HttpGet("export-to-excel")]
        public async Task<ActionResult> ExportToExcelAsync()
        {
            var stream = await _tripExpenseService.ExportTripExpensesToExcelAsync();
            var fileName = "items.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTripExpenseAsync(Guid id, [FromBody] TripExpenseRequest tripExpenseRequest)
        {
            await _tripExpenseService.UpdateTripExpenseAsync(id, tripExpenseRequest.AdvanceReportId, tripExpenseRequest.TripExpenseTypeId, tripExpenseRequest.Amount, tripExpenseRequest.Date, tripExpenseRequest.Description);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTripExpenseAsync(Guid id)
        {
            await _tripExpenseService.DeleteTripExpenseAsync(id);

            return Ok();
        }
    }
}
