using Data.Data;
using Data.Entities;
using Data.Filters;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
	public class GameRepository(GamestoreDBContext context) : GenericRepository<Game>(context), IGameRepository
	{
		public async Task<IEnumerable<Game>> GetAllWithFilterAsync(GameFilter filter)
		{
			var query = Context.Games.AsQueryable();

			if (filter.Genres != null && filter.Genres.Count != 0)
			{
				query = query.Where(g => g.Genres.Any(genre => filter.Genres.Contains(genre.Id)));
			}

			if (filter.Platforms != null && filter.Platforms.Count != 0)
			{
				query = query.Where(g => g.Platforms.Any(platform => filter.Platforms.Contains(platform.Id)));
			}

			if (filter.Publishers != null && filter.Publishers.Count != 0)
			{
				query = query.Where(g => filter.Publishers.Contains(g.PublisherId));
			}

			if (filter.MaxPrice.HasValue)
			{
				query = query.Where(g => g.Price <= filter.MaxPrice.Value);
			}

			if (filter.MinPrice.HasValue)
			{
				query = query.Where(g => g.Price >= filter.MinPrice.Value);
			}

			if (!string.IsNullOrEmpty(filter.DatePublishing))
			{
				var now = DateTime.UtcNow;

				query = filter.DatePublishing switch
				{
					"last week" => query.Where(g => g.PublishDate >= now.AddDays(-7)),
					"last month" => query.Where(g => g.PublishDate >= now.AddMonths(-1)),
					"last year" => query.Where(g => g.PublishDate >= now.AddYears(-1)),
					"2 years" => query.Where(g => g.PublishDate >= now.AddYears(-2)),
					"3 years" => query.Where(g => g.PublishDate >= now.AddYears(-3)),
					_ => query // No filtering if the value doesn't match
				};
			}

			if (!string.IsNullOrEmpty(filter.Name))
			{
				query = query.Where(g => g.Name.Contains(filter.Name));
			}

			if (!string.IsNullOrEmpty(filter.Sort))
			{
				query = filter.Sort switch
				{
					"Price ASC" => query.OrderBy(g => g.Price),
					"Price DESC" => query.OrderByDescending(g => g.Price),
					"Most commented" => query.OrderByDescending(g => g.Comments.Count),
					"Most popular" => query.OrderByDescending(g => g.Views),
					"New" => query.OrderByDescending(g => g.PublishDate),
					_ => query
				};
			}


			int pageCount = 10;

			if (!string.IsNullOrEmpty(filter.PageCount) && !filter.PageCount.Equals("all"))
			{
				var parse = int.TryParse(filter.PageCount, out pageCount);
				
				if (!parse)
				{
					pageCount = 10;
				}
			}

			query = query.Skip((filter.Page - 1) * pageCount)
				 .Take(pageCount);

			return await query.ToListAsync();
		}
	}
}
