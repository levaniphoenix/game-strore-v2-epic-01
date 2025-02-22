using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
	public class PlatformService(IUnitOfWork unitOfWork, IMapper mapper) : IPlatformService
	{
		public async Task AddAsync(PlatformModel model)
		{
			await ValidatePlatform(model);
			var platform = mapper.Map<Platform>(model);
			await unitOfWork.PlatformRepository!.AddAsync(platform);
			await unitOfWork.SaveAsync();
		}

		public async Task DeleteAsync(object modelId)
		{
			await unitOfWork.PlatformRepository!.DeleteByIdAsync(modelId);
			await unitOfWork.SaveAsync();
		}

		public async Task<IEnumerable<PlatformModel>> GetAllAsync()
		{
			var platforms = await unitOfWork.PlatformRepository!.GetAllAsync();
			return mapper.Map<IEnumerable<PlatformModel>>(platforms);
		}

		public async Task<PlatformModel?> GetByIdAsync(object id)
		{
			var platform = await unitOfWork.PlatformRepository!.GetByIDAsync(id);
			return mapper.Map<PlatformModel?>(platform);
		}

		public async Task<IEnumerable<GameModel?>> GetGamesByGenreIdAsync(Guid id)
		{
			var games = await unitOfWork.GameRepository!.GetAllAsync(g => g.Platforms.Select(g => g.Id).Contains(id));
			return mapper.Map<IEnumerable<GameModel?>>(games);
		}

		public async Task UpdateAsync(PlatformModel model)
		{
			await ValidatePlatform(model);
			var platform = mapper.Map<Platform>(model);
			unitOfWork.PlatformRepository!.Update(platform);
			await unitOfWork.SaveAsync();
		}

		public async Task ValidatePlatform(PlatformModel model)
		{
			var existingPlatform = (await unitOfWork.PlatformRepository!.GetAllAsync(g => g.Type == model.Platform.Type)).SingleOrDefault();

			if (existingPlatform is not null) throw new GameStoreValidationException(ErrorMessages.PlatformTypeAlreadyExists);
		}
	}
}
