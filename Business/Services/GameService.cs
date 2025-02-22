using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services;

public class GameService(IUnitOfWork unitOfWork, IMapper mapper) : IGameService
{

	public async Task AddAsync(GameModel model)
	{
		ArgumentNullException.ThrowIfNull(model);
		model.Game.Id = null;
		await ValidateGame(model);

		var game = mapper.Map<Game>(model);

		game.Key ??= GenerateKey(model.Game.Name);

		await PopulateNavigationProperties(model, game);

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

	public async Task<GameModel?> GetByKeyAsync(string key)
	{
		if (key.IsNullOrEmpty())
		{
			throw new ArgumentNullException(nameof(key));
		}

		var games = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key)).SingleOrDefault();
		return mapper.Map<GameModel?>(games);
	}

	public async Task<IEnumerable<GenreModel>> GetGenresByGamekey(string key)
	{
		if (key.IsNullOrEmpty())
		{
			throw new ArgumentNullException(nameof(key));
		}

		var genres = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key, includeProperties: "Genres")).SelectMany(g => g.Genres);
		return mapper.Map<IEnumerable<GenreModel>>(genres);
	}

	public async Task<IEnumerable<PlatformModel>> GetPlatformsByGamekey(string key)
	{
		ArgumentException.ThrowIfNullOrEmpty(key);
		var platforms = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key, includeProperties: "Platforms")).SelectMany(g => g.Platforms);
		return mapper.Map<IEnumerable<PlatformModel>>(platforms);
	}

	public async Task<GameModel?> GetByIdAsync(object id)
	{
		ArgumentNullException.ThrowIfNull(id);
		var game = await unitOfWork.GameRepository!.GetByIDAsync(id);
		return mapper.Map<GameModel?>(game);
	}

	public async Task<GameModel?> GetByNameAsync(string gameName)
	{
		ArgumentException.ThrowIfNullOrEmpty(gameName);

		var game = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Name == gameName)).SingleOrDefault();
		return mapper.Map<GameModel>(game);
	}

	public async Task UpdateAsync(GameModel model)
	{
		ArgumentNullException.ThrowIfNull(model);

		await ValidateGame(model);

		var game = mapper.Map<Game>(model);
		game.Key ??= GenerateKey(model.Game.Name);

		await PopulateNavigationProperties(model, game);

		unitOfWork.GameRepository!.Update(game);
		await unitOfWork.SaveAsync();
	}

	public string GenerateKey(string gameName)
	{
		ArgumentNullException.ThrowIfNull(gameName);

		var key = gameName.Split(" ").Select(s => string.Concat(s[0].ToString().ToUpperInvariant(), s.AsSpan(1)));

		return string.Join("", key);
	}

	public async Task ValidateGame(GameModel model)
	{
		var existingGame = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Name == model.Game.Name)).SingleOrDefault();

		if (existingGame is not null)
		{
			throw new GameStoreValidationException(ErrorMessages.GameNameAlreadyExists);
		}

		var key = model.Game.Key;

		key ??= GenerateKey(model.Game.Name);

		existingGame = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key)).SingleOrDefault();

		if (existingGame is not null)
		{
			throw new GameStoreValidationException(ErrorMessages.GameKeyAlreadyExists);
		}
	}

	public async Task PopulateNavigationProperties(GameModel model, Game game)
	{
		if (model.PlatformIds is not null)
		{
			var platforms = (await unitOfWork.PlatformRepository!.GetAllAsync(p => model.PlatformIds.Contains(p.Id))).ToList();

			foreach (var platformID in model.PlatformIds)
			{
				if (!platforms.Select(p => p.Id).Contains(platformID))
				{
					throw new GameStoreValidationException(string.Format(ErrorMessages.PlatformDoesNotExist, platformID));
				}
			}
			game.Platforms = platforms;
		}

		if (model.GenreIds is not null)
		{
			var genres = (await unitOfWork.GenreRepository!.GetAllAsync(p => model.GenreIds.Contains(p.Id))).ToList();

			foreach (var genreID in model.GenreIds)
			{
				if (!genres.Select(p => p.Id).Contains(genreID))
				{
					throw new GameStoreValidationException(string.Format(ErrorMessages.GenreDoesNotExist, genreID));
				}
			}

			game.Genres = genres;
		}
	}
}
