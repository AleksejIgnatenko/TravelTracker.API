using TravelTracker.Core.Exceptions;
using TravelTracker.Core.Models.CommandModels;
using TravelTracker.Core.Abstractions;

namespace TravelTracker.Application.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IValidationService _validationService;

        public CommandService(ICommandRepository commandRepository, IValidationService validationService)
        {
            _commandRepository = commandRepository;
            _validationService = validationService;
        }

        public async Task CreateCommandAsync(string title, string description, string dateIssued)
        {
            var command = new CommandEntity
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                DateIssued = dateIssued
            };

            var validationErrors = _validationService.Validation(command);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _commandRepository.CreateAsync(command);
        }

        public async Task<IEnumerable<CommandEntity>> GetAllCommandsAsync()
        {
            return await _commandRepository.GetAllAsync();
        }

        public async Task UpdateCommandAsync(Guid id, string title, string description, string dateIssued)
        {
            var command = new CommandEntity
            {
                Id = id,
                Title = title,
                Description = description,
                DateIssued = dateIssued
            };

            var validationErrors = _validationService.Validation(command);
            if (validationErrors.Count != 0)
            {
                throw new ValidationException(validationErrors);
            }

            await _commandRepository.UpdateAsync(command);
        }

        public async Task DeleteCommandAsync(Guid id)
        {
            await _commandRepository.DeleteAsync(await _commandRepository.GetByIdAsync(id));
        }
    }
}
