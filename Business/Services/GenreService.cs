using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
	public class GenreService(IUnitOfWork unitOfWork, IMapper mapper) : IGenreService
	{
		private readonly IUnitOfWork unitOfWork = unitOfWork;

		private IMapper Mapper { get; } = mapper;

		public async Task AddAsync(GenreModel model)
		{
			await ValidateGenre(model);
			var genre = Mapper.Map<Genre>(model);
			await unitOfWork.GenreRepository!.AddAsync(genre);
			await unitOfWork.SaveAsync();
		}

		public async Task DeleteAsync(object modelId)
		{
			await unitOfWork.GenreRepository!.DeleteByIdAsync(modelId);
			await unitOfWork.SaveAsync();
		}

		public async Task<IEnumerable<GenreModel>> GetAllAsync()
		{
			var genres = await unitOfWork.GenreRepository!.GetAllAsync();
			return Mapper.Map<IEnumerable<GenreModel>>(genres);
		}

		public async Task<IEnumerable<GenreModel?>> GetGenresByParentId(Guid id)
		{
			var genres = await unitOfWork.GenreRepository!.GetAllAsync(g => g.ParentGenreId == id);
			return Mapper.Map<IEnumerable<GenreModel?>>(genres);
		}

		public async Task<GenreModel?> GetByIdAsync(object id)
		{
			var genre = await unitOfWork.GenreRepository!.GetByIDAsync(id);
			return Mapper.Map<GenreModel?>(genre);
		}

		public async Task<IEnumerable<GameModel?>> GetGamesByGenreIdAsync(Guid id)
		{
			var games = await unitOfWork.GameRepository!.GetAllAsync(g => g.Genres.Select(g => g.Id).Contains(id));
			return Mapper.Map<IEnumerable<GameModel?>>(games);
		}

		public async Task UpdateAsync(GenreModel model)
		{
			await ValidateGenre(model);
			var genre = Mapper.Map<Genre>(model);
			unitOfWork.GenreRepository!.Update(genre);
			await unitOfWork.SaveAsync();
		}

		public async Task ValidateGenre(GenreModel model)
		{
			var existingGenre = (await unitOfWork.GenreRepository!.GetAllAsync(g => g.Name == model.Genre.Name)).SingleOrDefault();

			if (existingGenre is not null) throw new GameStoreValidationException(ErrorMessages.GenreNameAlreadyExists);
		}
	}

}
