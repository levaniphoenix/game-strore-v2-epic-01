using Data.Data;
using Data.Entities;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Data.Repositories
{
	public class GenreRepository : GenericRepository<Genre>
	{
		public GenreRepository(GamestoreDBContext context) : base(context)
		{
		}

		override public async Task DeleteByIdAsync(object id)
		{
			var genre = await context.Genres.Include(g => g.Children).Where(g => g.Id == (Guid)id).SingleAsync();
			Delete(genre);
		}

		override public async void Delete(Genre entityToDelete)
		{
			var subgenres = await context.Genres.Where(g => g.ParentGenreId == entityToDelete.Id).ToListAsync();
			foreach (var subgenre in subgenres)
			{
				base.Delete(subgenre);
			}
			base.Delete(entityToDelete);
		}
	}
}
