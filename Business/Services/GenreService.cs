using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services
{
	public class GenreService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GenreService> logger) : IGenreService
	{
		public async Task AddAsync(GenreModel model)
		{
			logger.LogInformation("Adding a new genre: {GenreName}", model.Genre.Name);

			await ValidateGenre(model);
			
			var genre = mapper.Map<Genre>(model);
			await unitOfWork.GenreRepository!.AddAsync(genre);
			await unitOfWork.SaveAsync();
			logger.LogInformation("Genre {GenreName} added successfully", model.Genre.Name);
		}

		public async Task DeleteAsync(object modelId)
		{
			logger.LogInformation("Deleting genre with ID: {GenreId}", modelId);
			await unitOfWork.GenreRepository!.DeleteByIdAsync(modelId);
			await unitOfWork.SaveAsync();
			logger.LogInformation("Genre with ID {GenreId} deleted successfully", modelId);
		}

		public async Task<IEnumerable<GenreModel>> GetAllAsync()
		{
			logger.LogInformation("Fetching all genres");
			var genres = await unitOfWork.GenreRepository!.GetAllAsync();
			logger.LogInformation("Fetched {Count} genres", genres.Count());
			return mapper.Map<IEnumerable<GenreModel>>(genres);
		}

		public async Task<IEnumerable<GenreModel?>> GetGenresByParentId(Guid id)
		{
			logger.LogInformation("Fetching genres by parent ID: {ParentId}", id);
			var genres = await unitOfWork.GenreRepository!.GetAllAsync(g => g.ParentGenreId == id);
			logger.LogInformation("Fetched {Count} genres for parent ID: {ParentId}", genres.Count(), id);
			return mapper.Map<IEnumerable<GenreModel?>>(genres);
		}

		public async Task<GenreModel?> GetByIdAsync(object id)
		{
			logger.LogInformation("Fetching genre by ID: {GenreId}", id);
			var genre = await unitOfWork.GenreRepository!.GetByIDAsync(id);
			logger.LogInformation("Fetched genre by ID: {GenreId}, Exists: {Exists}", id, genre is not null);
			return mapper.Map<GenreModel?>(genre);
		}

		public async Task<IEnumerable<GameModel?>> GetGamesByGenreIdAsync(Guid id)
		{
			logger.LogInformation("Fetching games by genre ID: {GenreId}", id);
			var games = await unitOfWork.GameRepository!.GetAllAsync(g => g.Genres.Select(g => g.Id).Contains(id));
			logger.LogInformation("Fetched {Count} games for genre ID: {GenreId}", games.Count(), id);
			return mapper.Map<IEnumerable<GameModel?>>(games);
		}

		public async Task UpdateAsync(GenreModel model)
		{
			logger.LogInformation("Updating genre: {GenreName}", model.Genre.Name);

			await ValidateGenre(model);
			var genre = mapper.Map<Genre>(model);
			unitOfWork.GenreRepository!.Update(genre);
			await unitOfWork.SaveAsync();

			logger.LogInformation("Genre {GenreName} updated successfully", model.Genre.Name);
		}

		public async Task ValidateGenre(GenreModel model)
		{
			logger.LogInformation("Validating genre: {GenreName}", model.Genre.Name);

			var existingGenre = (await unitOfWork.GenreRepository!.GetAllAsync(g => g.Name == model.Genre.Name)).SingleOrDefault();

			if (existingGenre is not null)
			{
				logger.LogWarning("Validation failed: Genre name {GenreName} already exists", model.Genre.Name);
				throw new GameStoreValidationException(ErrorMessages.GenreNameAlreadyExists);
			}

			logger.LogInformation("Validation successful for genre: {GenreName}", model.Genre.Name);
		}
	}
}
