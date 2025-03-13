using Microsoft.AspNetCore.Mvc;
using TravelTracker.Core.Abstractions;
using TravelTracker.Core.Models.CityModels;

namespace TravelTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCityAsync([FromBody] CityRequest cityRequest)
        {
            await _cityService.CreateCityAsync(cityRequest.Country, cityRequest.Name);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCitiesAsync()
        {
            var cities = await _cityService.GetAllCitiesAsync();

            var response = cities.Select(c => new CityResponse(c.Id, c.Country, c.Name));

            return Ok(response);
        }

        [HttpGet("export-to-excel")]
        public async Task<ActionResult> ExportToExcelAsync()
        {
            var stream = await _cityService.ExportCitiesToExcelAsync();
            var fileName = "items.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateCityAsync(Guid id, [FromBody] CityRequest cityRequest)
        {
            await _cityService.UpdateCityAsync(id, cityRequest.Country, cityRequest.Name);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCityAsync(Guid id)
        {
            await _cityService.DeleteCityAsync(id);

            return Ok();
        }
    }
}
