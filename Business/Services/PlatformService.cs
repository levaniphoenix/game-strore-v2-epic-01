using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services
{
	public class PlatformService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PlatformService> logger) : IPlatformService
	{
		public async Task AddAsync(PlatformModel model)
		{
			logger.LogInformation("Adding a new platform: {PlatformType}", model.Platform.Type);

			await ValidatePlatform(model);
			var platform = mapper.Map<Platform>(model);
			await unitOfWork.PlatformRepository!.AddAsync(platform);
			await unitOfWork.SaveAsync();

			logger.LogInformation("Platform {PlatformType} added successfully", model.Platform.Type);
		}

		public async Task DeleteAsync(object modelId)
		{
			logger.LogInformation("Deleting platform with ID: {PlatformId}", modelId);

			await unitOfWork.PlatformRepository!.DeleteByIdAsync(modelId);
			await unitOfWork.SaveAsync();

			logger.LogInformation("Platform with ID {PlatformId} deleted successfully", modelId);
		}

		public async Task<IEnumerable<PlatformModel>> GetAllAsync()
		{
			logger.LogInformation("Fetching all platforms");

			var platforms = await unitOfWork.PlatformRepository!.GetAllAsync();
			logger.LogInformation("Fetched {Count} platforms", platforms.Count());

			return mapper.Map<IEnumerable<PlatformModel>>(platforms);
		}

		public async Task<PlatformModel?> GetByIdAsync(object id)
		{
			logger.LogInformation("Fetching platform by ID: {PlatformId}", id);

			var platform = await unitOfWork.PlatformRepository!.GetByIDAsync(id);
			logger.LogInformation("Fetched platform by ID: {PlatformId}, Exists: {Exists}", id, platform is not null);

			return mapper.Map<PlatformModel?>(platform);
		}

		public async Task<IEnumerable<GameModel?>> GetGamesByPlatformIdAsync(Guid id)
		{
			logger.LogInformation("Fetching games for platform ID: {PlatformId}", id);

			var games = await unitOfWork.GameRepository!.GetAllAsync(g => g.Platforms.Select(g => g.Id).Contains(id));
			logger.LogInformation("Fetched {Count} games for platform ID: {PlatformId}", games.Count(), id);

			return mapper.Map<IEnumerable<GameModel?>>(games);
		}

		public async Task UpdateAsync(PlatformModel model)
		{
			logger.LogInformation("Updating platform: {PlatformType}", model.Platform.Type);

			await ValidatePlatform(model);
			var platform = mapper.Map<Platform>(model);
			unitOfWork.PlatformRepository!.Update(platform);
			await unitOfWork.SaveAsync();
			
			logger.LogInformation("Platform {PlatformType} updated successfully", model.Platform.Type);
		}

		public async Task ValidatePlatform(PlatformModel model)
		{
			logger.LogInformation("Validating platform: {PlatformType}", model.Platform.Type);

			var existingPlatform = (await unitOfWork.PlatformRepository!.GetAllAsync(g => g.Type == model.Platform.Type)).SingleOrDefault();

			if (existingPlatform is not null)
			{
				logger.LogError("Validation failed: Platform type {PlatformType} already exists", model.Platform.Type);
				throw new GameStoreValidationException(ErrorMessages.PlatformTypeAlreadyExists);
			}

			logger.LogInformation("Validation successful for platform: {PlatformType}", model.Platform.Type);
		}
	}
}
