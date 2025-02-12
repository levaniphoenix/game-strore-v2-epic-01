using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
	public class GenreService : IGenreService
	{
		private readonly IUnitOfWork unitOfWork;

		public GenreService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task AddAsync(Genre model)
		{
			await unitOfWork.GenreRepository!.AddAsync(model);
		}

		public async Task DeleteAsync(object modelId)
		{
			await unitOfWork.GenreRepository!.DeleteByIdAsync(modelId);
		}

		public async Task<IEnumerable<Genre>> GetAllAsync()
		{
			return await unitOfWork.GenreRepository!.GetAllAsync();
		}

		public async Task<Genre?> GetByIdAsync(object id)
		{
			return await unitOfWork.GenreRepository!.GetByIDAsync(id);
		}

		public Task UpdateAsync(Genre model)
		{
			unitOfWork.GenreRepository!.Update(model);
			return Task.CompletedTask;
		}
	}

}
