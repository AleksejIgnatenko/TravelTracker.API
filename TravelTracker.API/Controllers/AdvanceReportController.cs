using Microsoft.AspNetCore.Mvc;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.AdvanceReportModels;

namespace TravelTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvanceReportController : ControllerBase
    {
        private readonly IAdvanceReportService _advanceReportService;

        public AdvanceReportController(IAdvanceReportService advanceReportService)
        {
            _advanceReportService = advanceReportService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdvanceReportAsync([FromBody] AdvanceReportRequest advanceReportRequest)
        {
            await _advanceReportService.CreateAdvanceReportAsync(advanceReportRequest.TripCertificateId, advanceReportRequest.TotalAmount, advanceReportRequest.DateOfDelivery);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAdvanceReportsAsync()
        {
            var advanceReports = await _advanceReportService.GetAllAdvanceReportsAsync();

            var response = advanceReports.Select(a => new AdvanceReportResponse(a.Id, a.TripCertificate.Id, a.TotalAmount, a.DateOfDelivery));

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAdvanceReportAsync(Guid id, [FromBody] AdvanceReportRequest advanceReportRequest)
        {
            await _advanceReportService.UpdateAdvanceReportAsync(id, advanceReportRequest.TripCertificateId, advanceReportRequest.TotalAmount, advanceReportRequest.DateOfDelivery);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAdvanceReportAsync(Guid id)
        {
            await _advanceReportService.DeleteAdvanceReportAsync(id);

            return Ok();
        }
    }
}
