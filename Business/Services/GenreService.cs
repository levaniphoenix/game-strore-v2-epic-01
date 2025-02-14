using AutoMapper;
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
			var genre = mapper.Map<Genre>(model);
			await unitOfWork.GenreRepository!.AddAsync(genre);
		}

		public async Task DeleteAsync(object modelId)
		{
			await unitOfWork.GenreRepository!.DeleteByIdAsync(modelId);
		}

		public async Task<IEnumerable<GenreModel>> GetAllAsync()
		{
			var genres = await unitOfWork.GenreRepository!.GetAllAsync();
			return mapper.Map<IEnumerable<GenreModel>>(genres);
		}

		public async Task<GenreModel?> GetByIdAsync(object id)
		{
			var genre = await unitOfWork.GenreRepository!.GetByIDAsync(id);
			return mapper.Map<GenreModel?>(genre);
		}

		public Task UpdateAsync(GenreModel model)
		{
			var genre = mapper.Map<Genre>(model);
			unitOfWork.GenreRepository!.Update(genre);
			return Task.CompletedTask;
		}
	}

}
