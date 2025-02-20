using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
	public class GenreService : IGenreService
	{
		private readonly IUnitOfWork unitOfWork;

		private IMapper mapper { get; }

		public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task AddAsync(GenreModel model)
		{
			await ValidateGenre(model);
			var genre = mapper.Map<Genre>(model);
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
			return mapper.Map<IEnumerable<GenreModel>>(genres);
		}

		public async Task<IEnumerable<GenreModel?>> GetGenresByParentId(Guid id)
		{
			var genres = await unitOfWork.GenreRepository!.GetAllAsync(g => g.ParentGenreId == id);
			return mapper.Map<IEnumerable<GenreModel?>>(genres);
		}

		public async Task<GenreModel?> GetByIdAsync(object id)
		{
			var genre = await unitOfWork.GenreRepository!.GetByIDAsync(id);
			return mapper.Map<GenreModel?>(genre);
		}

		public async Task<IEnumerable<GameModel?>> GetGamesByGenreIdAsync(Guid id)
		{
			var games =await unitOfWork.GameRepository!.GetAllAsync(g => g.Genres.Select(g => g.Id).Contains(id));
			return mapper.Map<IEnumerable<GameModel?>>(games);
		}

		public async Task UpdateAsync(GenreModel model)
		{
			await ValidateGenre(model);
			var genre = mapper.Map<Genre>(model);
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
