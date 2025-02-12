using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
	public class GameService : IGameService
	{
		private readonly IUnitOfWork unitOfWork;

		public GameService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task AddAsync(Game model)
		{
			if(model == null) throw new ArgumentNullException(nameof(model));
			await unitOfWork.GameRepository!.AddAsync(model);
			await unitOfWork.SaveAsync();
		}

		public async Task DeleteAsync(object modelId)
		{
			await unitOfWork.GameRepository!.DeleteByIdAsync(modelId);
			await unitOfWork.SaveAsync();
		}

		public async Task<IEnumerable<Game>> GetAllAsync()
		{
			return await unitOfWork.GameRepository!.GetAllAsync();
		}

		public async Task<Game?> GetByIdAsync(object id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			return await unitOfWork.GameRepository!.GetByIDAsync(id);
		}

		public Task UpdateAsync(Game model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			unitOfWork.GameRepository!.Update(model);
			return Task.CompletedTask;
		}
	}
}
