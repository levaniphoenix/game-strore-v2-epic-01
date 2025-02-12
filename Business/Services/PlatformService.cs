using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
	public class PlatformService : IPlatformService
	{
		private readonly IUnitOfWork unitOfWork;

		public PlatformService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task AddAsync(Platform model)
		{
			await unitOfWork.PlatformRepository!.AddAsync(model);
		}

		public async Task DeleteAsync(object modelId)
		{
			await unitOfWork.PlatformRepository!.DeleteByIdAsync(modelId);
		}

		public async Task<IEnumerable<Platform>>GetAllAsync()
		{
			return await unitOfWork.PlatformRepository!.GetAllAsync();
		}

		public async Task<Platform?> GetByIdAsync(object id)
		{
			return await unitOfWork.PlatformRepository!.GetByIDAsync(id);
		}

		public Task UpdateAsync(Platform model)
		{
			unitOfWork.PlatformRepository!.Update(model);
			return Task.CompletedTask;
		}
	}
}
