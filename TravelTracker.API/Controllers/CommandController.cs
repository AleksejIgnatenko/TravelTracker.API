using Microsoft.AspNetCore.Mvc;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.CommandModels;

namespace TravelTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _commandService;

        public CommandController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCommandAsync([FromBody] CommandRequest commandRequest)
        {
            await _commandService.CreateCommandAsync(commandRequest.Title, commandRequest.Description, commandRequest.DateIssued);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCommandsAsync()
        {
            var commands = await _commandService.GetAllCommandsAsync();

            var response = commands.Select(c => new CommandResponse(c.Id, c.Title, c.Description, c.DateIssued));

            return Ok(response);
        }

        [HttpGet("export-to-excel")]
        public async Task<ActionResult> ExportToExcelAsync()
        {
            var stream = await _commandService.ExportCommandsToExcelAsync();
            var fileName = "items.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet("export-date-quantity-chart-to-excel")]
        public async Task<ActionResult> ExportDateQuantityChartToExcelAsync()
        {
            var stream = await _commandService.ExportDateQuantityChartToExcelAsync();
            var fileName = "graphic.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateCommandAsync(Guid id, [FromBody] CommandRequest commandRequest)
        {
            await _commandService.UpdateCommandAsync(id, commandRequest.Title, commandRequest.Description, commandRequest.DateIssued);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCommandAsync(Guid id)
        {
            await _commandService.DeleteCommandAsync(id);

            return Ok();
        }
    }
}
