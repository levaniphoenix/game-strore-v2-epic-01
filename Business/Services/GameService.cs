using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
	public class GameService : IGameService
	{
		private readonly IUnitOfWork unitOfWork;

		private IMapper mapper { get; }

		public GameService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
		}

		public async Task AddAsync(GameModel model)
		{
			if(model == null) throw new ArgumentNullException(nameof(model));
			
			var game = mapper.Map<Game>(model);

			await unitOfWork.GameRepository!.AddAsync(game);
			await unitOfWork.SaveAsync();
		}

		public async Task DeleteAsync(object modelId)
		{
			await unitOfWork.GameRepository!.DeleteByIdAsync(modelId);
			await unitOfWork.SaveAsync();
		}

		public async Task<IEnumerable<GameModel>> GetAllAsync()
		{
			var games = await unitOfWork.GameRepository!.GetAllAsync();
			return mapper.Map<IEnumerable<GameModel>>(games);
		}

		public async Task<GameModel?> GetByIdAsync(object id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			var game = await unitOfWork.GameRepository!.GetByIDAsync(id);
			return mapper.Map<GameModel?>(game);
		}

		public Task UpdateAsync(GameModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			var game = mapper.Map<Game>(model);
			unitOfWork.GameRepository!.Update(game);
			return Task.CompletedTask;
		}
	}
}
