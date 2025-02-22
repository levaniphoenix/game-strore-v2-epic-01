using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
	public class GenreRepository(GamestoreDBContext context) : GenericRepository<Genre>(context)
	{
		public override async Task DeleteByIdAsync(object id)
		{
			var genre = await Context.Genres.Include(g => g.Children).Where(g => g.Id == (Guid)id).SingleAsync();
			Delete(genre);
		}

		public override async void Delete(Genre entityToDelete)
		{
			var subgenres = await Context.Genres.Where(g => g.ParentGenreId == entityToDelete.Id).ToListAsync();
			foreach (var subgenre in subgenres)
			{
				base.Delete(subgenre);
			}
			base.Delete(entityToDelete);
		}
	}
}
