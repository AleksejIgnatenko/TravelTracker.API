using Microsoft.AspNetCore.Mvc;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.TripExpenseTypeModels;

namespace TravelTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripExpenseTypeController : ControllerBase
    {
        private readonly ITripExpenseTypeService _tripExpenseTypeService;

        public TripExpenseTypeController(ITripExpenseTypeService tripExpenseTypeService)
        {
            _tripExpenseTypeService = tripExpenseTypeService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTripExpenseTypeAsync([FromBody] TripExpenseTypeRequest tripExpenseTypeRequest)
        {
            await _tripExpenseTypeService.CreateTripExpenseTypeAsync(tripExpenseTypeRequest.Name, tripExpenseTypeRequest.Standard);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTripExpenseTypesAsync()
        {
            var tripExpenseTypes = await _tripExpenseTypeService.GetAllTripExpenseTypesAsync();

            var response = tripExpenseTypes.Select(t => new TripExpenseTypeResponse(t.Id, t.Name, t.Standard));

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTripExpenseTypeAsync(Guid id, [FromBody] TripExpenseTypeRequest tripExpenseTypeRequest)
        {
            await _tripExpenseTypeService.UpdateTripExpenseTypeAsync(id, tripExpenseTypeRequest.Name, tripExpenseTypeRequest.Standard);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTripExpenseTypeAsync(Guid id)
        {
            await _tripExpenseTypeService.DeleteTripExpenseTypeAsync(id);

            return Ok();
        }
    }
}
