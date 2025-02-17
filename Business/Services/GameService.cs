using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.IdentityModel.Tokens;

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
			
			await ValidateGame(model);

			var game = mapper.Map<Game>(model);
			game.Key = GenerateKey(model.Name);
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

		public async Task UpdateAsync(GameModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			
			await ValidateGame(model);

			var game = mapper.Map<Game>(model);
			game.Key = GenerateKey(model.Name);
			unitOfWork.GameRepository!.Update(game);
			await unitOfWork.SaveAsync();
		}

		public string GenerateKey(string gameName)
		{
			if(gameName.IsNullOrEmpty()) throw new ArgumentNullException(nameof(gameName));

			var key = gameName.Split(" ").Select(s => s.ToUpperInvariant());

			return string.Join("", key);
		}

		public async Task ValidateGame(GameModel model)
		{
			var existingGame = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Name == model.Name)).Single();

			if (existingGame is not null) throw new GameStoreException(ErrorMessages.GameNameAlreadyExists);

			var key = GenerateKey(model.Name);

			existingGame = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key)).Single();

			if (existingGame is not null) throw new GameStoreException(ErrorMessages.GameKeyAlreadyExists);
		}
	}
}
