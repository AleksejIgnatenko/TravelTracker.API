using Microsoft.AspNetCore.Mvc;
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
