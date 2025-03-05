using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.CityModels;
using TravelTracker.Core.Abstractions;

namespace TravelTracker.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IValidationService _validationService;

        public CityService(ICityRepository cityRepository, IValidationService validationService)
        {
            _cityRepository = cityRepository;
            _validationService = validationService;
        }

        public async Task CreateCityAsync(string country, string name)
        {
            var city = new CityEntity
            {
                Id = Guid.NewGuid(),
                Country = country,
                Name = name,
            };

            var validationErrors = _validationService.Validation(city);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _cityRepository.CreateAsync(city);
        }

        public async Task<IEnumerable<CityEntity>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task UpdateCityAsync(Guid id, string country, string name)
        {
            var city = new CityEntity
            {
                Id = id,
                Country = country,
                Name = name,
            };

            var validationErrors = _validationService.Validation(city);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteCityAsync(Guid id)
        {
            await _cityRepository.DeleteAsync(await _cityRepository.GetByIdAsync(id));
        }
    }
}
