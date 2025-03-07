using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class GameService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GameService> logger) : IGameService
{
	public async Task<int> GetTotalGamesCountAsync()
	{
		logger.LogInformation("Fetching total games count");
		return await unitOfWork.GameRepository.GetTotalCountAsync();
	}

	public Task AddAsync(GameModel model)
	{
		ArgumentNullException.ThrowIfNull(model);
		model.Game.Id = null;

		async Task AddGame()
		{
			logger.LogInformation("Adding a new game: {GameName}", model.Game.Name);
			await ValidateGame(model);

			var game = mapper.Map<Game>(model);

			game.Key ??= GenerateKey(model.Game.Name);

			await PopulateNavigationProperties(model, game);

			await unitOfWork.GameRepository!.AddAsync(game);
			await unitOfWork.SaveAsync();
			logger.LogInformation("Game {GameName} added successfully with key {GameKey}", model.Game.Name, game.Key);
		}

		return AddGame();
	}

	public async Task DeleteAsync(object modelId)
	{
		logger.LogInformation("Deleting game with ID: {GameId}", modelId);
		await unitOfWork.GameRepository!.DeleteByIdAsync(modelId);
		await unitOfWork.SaveAsync();
		logger.LogInformation("Game with ID {GameId} deleted successfully", modelId);
	}

	public async Task<IEnumerable<GameModel>> GetAllAsync()
	{
		logger.LogInformation("Fetching all games");
		var games = await unitOfWork.GameRepository!.GetAllAsync();
		return mapper.Map<IEnumerable<GameModel>>(games);
	}

	public Task<GameModel?> GetByKeyAsync(string key)
	{
		ArgumentException.ThrowIfNullOrEmpty(key);
		logger.LogInformation("Fetching game by key: {GameKey}", key);

		async Task<GameModel?> getGameByIdAsync()
		{
			var games = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key)).SingleOrDefault();
			return mapper.Map<GameModel?>(games);
		}

		return getGameByIdAsync();
	}

	public Task<IEnumerable<GenreModel>> GetGenresByGamekey(string key)
	{
		ArgumentException.ThrowIfNullOrEmpty(key);
		logger.LogInformation("Fetching genres by key: {GameKey}", key);
		async Task<IEnumerable<GenreModel>> getGenres()
		{
			var genres = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key, includeProperties: "Genres")).SelectMany(g => g.Genres);
			logger.LogInformation("Fetched {Count} genres for game key: {GameKey}", genres.Count(), key);
			return mapper.Map<IEnumerable<GenreModel>>(genres);
		}

		return getGenres();
	}

	public Task<IEnumerable<PlatformModel>> GetPlatformsByGamekey(string key)
	{
		ArgumentException.ThrowIfNullOrEmpty(key);
		logger.LogInformation("Fetching platforms by key: {GameKey}", key);

		async Task<IEnumerable<PlatformModel>> getPlatforms()
		{
			var platforms = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key, includeProperties: "Platforms")).SelectMany(g => g.Platforms);
			logger.LogInformation("Fetched {Count} platforms for game key: {GameKey}", platforms.Count(), key);
			return mapper.Map<IEnumerable<PlatformModel>>(platforms);
		}
		
		return getPlatforms();
	}

	public Task<GameModel?> GetByIdAsync(object id)
	{
		ArgumentNullException.ThrowIfNull(id);
		logger.LogInformation("Fetching game by ID: {GameId}", id);

		async Task<GameModel?> getById()
		{
			var game = await unitOfWork.GameRepository!.GetByIDAsync(id);
			return mapper.Map<GameModel?>(game);
		}

		return getById();
	}

	public Task<GameModel?> GetByNameAsync(string gameName)
	{
		ArgumentException.ThrowIfNullOrEmpty(gameName);
		logger.LogInformation("Fetching game by name: {GameName}", gameName);

		async Task<GameModel?> GetByName()
		{
			var game = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Name == gameName)).SingleOrDefault();
			return mapper.Map<GameModel>(game);
		}

		return GetByName();
	}

	public Task UpdateAsync(GameModel model)
	{
		ArgumentNullException.ThrowIfNull(model);
		logger.LogInformation("Updating game: {GameName}", model.Game.Name);

		async Task Update()
		{
			await ValidateGame(model);

			var game = mapper.Map<Game>(model);
			game.Key ??= GenerateKey(model.Game.Name);

			await PopulateNavigationProperties(model, game);

			unitOfWork.GameRepository!.Update(game);
			await unitOfWork.SaveAsync();
			logger.LogInformation("Game {GameName} updated successfully", model.Game.Name);
		}

		return Update();
	}

	public string GenerateKey(string gameName)
	{
		ArgumentNullException.ThrowIfNull(gameName);
		logger.LogInformation("Generating key for game name: {GameName}", gameName);

		var key = gameName.Split(" ").Select(s => string.Concat(s[0].ToString().ToUpperInvariant(), s.AsSpan(1)));
		var generatedKey = string.Join("", key);
		
		logger.LogInformation("Generated key: {GameKey} for game name: {GameName}", generatedKey, gameName);
		return generatedKey;
	}

	public async Task ValidateGame(GameModel model)
	{
		logger.LogInformation("Validating game: {GameName}", model.Game.Name);
		var existingGame = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Name == model.Game.Name)).SingleOrDefault();

		if (existingGame is not null)
		{
			logger.LogWarning("Validation failed: Game name {GameName} already exists", model.Game.Name);
			throw new GameStoreValidationException(ErrorMessages.GameNameAlreadyExists);
		}

		var key = model.Game.Key;

		key ??= GenerateKey(model.Game.Name);

		existingGame = (await unitOfWork.GameRepository!.GetAllAsync(g => g.Key == key)).SingleOrDefault();

		if (existingGame is not null)
		{
			logger.LogWarning("Validation failed: Game key {GameKey} already exists", key);
			throw new GameStoreValidationException(ErrorMessages.GameKeyAlreadyExists);
		}

		logger.LogInformation("Validation successful for game: {GameName}", model.Game.Name);
	}

	public async Task PopulateNavigationProperties(GameModel model, Game game)
	{
		logger.LogInformation("Populating navigation properties for game: {GameName}", model.Game.Name);
		if (model.PlatformIds is not null)
		{
			var platforms = (await unitOfWork.PlatformRepository!.GetAllAsync(p => model.PlatformIds.Contains(p.Id))).ToList();

			foreach (var platformID in model.PlatformIds)
			{
				if (!platforms.Select(p => p.Id).Contains(platformID))
				{
					logger.LogWarning("Validation failed: Platform ID {PlatformID} does not exist", platformID);
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
					logger.LogWarning("Validation failed: Genre ID {GenreID} does not exist", genreID);
					throw new GameStoreValidationException(string.Format(ErrorMessages.GenreDoesNotExist, genreID));
				}
			}

			game.Genres = genres;
		}
	}
}
