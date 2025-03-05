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
