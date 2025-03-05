using TravelTracker.Core.Models.CityModels;

namespace TravelTracker.Core.Abstractions
{
    public interface ICityService
    {
        Task CreateCityAsync(string country, string name);
        Task DeleteCityAsync(Guid id);
        Task<IEnumerable<CityEntity>> GetAllCitiesAsync();
        Task UpdateCityAsync(Guid id, string country, string name);
    }
}