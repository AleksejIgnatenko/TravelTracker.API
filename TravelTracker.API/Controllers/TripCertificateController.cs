using Microsoft.AspNetCore.Mvc;
using TravelTracker.Core.Abstractions;
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
            await _tripCertificateService.CreateTripCertificateAsync(tripCertificateRequest.EmployeeId, tripCertificateRequest.CommandId, tripCertificateRequest.CityId, tripCertificateRequest.StartDate, tripCertificateRequest.EndDate);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTripCertificatesAsync()
        {
            var tripCertificates = await _tripCertificateService.GetAllTripCertificatesAsync();

            var response = tripCertificates.Select(t => new TripCertificateResponse(t.Id, t.Employee.Id, 
                t.Employee.FirstName + " " + t.Employee.LastName + " " + t.Employee.MiddleName, t.Command.Id, 
                t.Command.Title, t.City.Id, t.City.Name, t.StartDate, t.EndDate));

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTripCertificateAsync(Guid id, [FromBody] TripCertificateRequest tripCertificateRequest)
        {
            await _tripCertificateService.UpdateTripCertificateAsync(id, tripCertificateRequest.EmployeeId, tripCertificateRequest.CommandId, tripCertificateRequest.CityId, tripCertificateRequest.StartDate, tripCertificateRequest.EndDate);

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
