using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
	public class PlatformService : IPlatformService
	{
		private readonly IUnitOfWork unitOfWork;

		private IMapper mapper { get; }

		public PlatformService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task AddAsync(PlatformModel model)
		{
			await ValidatePlatform(model);
			var platform = mapper.Map<Platform>(model);
			await unitOfWork.PlatformRepository!.AddAsync(platform);
		}

		public async Task DeleteAsync(object modelId)
		{
			await unitOfWork.PlatformRepository!.DeleteByIdAsync(modelId);
		}

		public async Task<IEnumerable<PlatformModel>>GetAllAsync()
		{
			var platforms = await unitOfWork.PlatformRepository!.GetAllAsync();
			return mapper.Map<IEnumerable<PlatformModel>>(platforms);
		}

		public async Task<PlatformModel?> GetByIdAsync(object id)
		{
			var platform = await unitOfWork.PlatformRepository!.GetByIDAsync(id);
			return mapper.Map<PlatformModel?>(platform);
		}

		public async Task UpdateAsync(PlatformModel model)
		{
			await ValidatePlatform(model);
			var platform = mapper.Map<Platform>(model);
			unitOfWork.PlatformRepository!.Update(platform);
		}

		public async Task ValidatePlatform(PlatformModel model)
		{
			var existingPlatform = (await unitOfWork.PlatformRepository!.GetAllAsync(g => g.Type == model.Type)).Single();

			if (existingPlatform is not null) throw new GameStoreException(ErrorMessages.PlatformTypeAlreadyExists);
		}
	}
}
