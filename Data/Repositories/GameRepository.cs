using Data.Data;
using Data.Entities;
using Common.Filters;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Common.Options;

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
					PublishingDateOptions.LastWeek => query.Where(g => g.PublishDate >= now.AddDays(-7)),
					PublishingDateOptions.LastMonth => query.Where(g => g.PublishDate >= now.AddMonths(-1)),
					PublishingDateOptions.LastYear => query.Where(g => g.PublishDate >= now.AddYears(-1)),
					PublishingDateOptions.LastTwoYears => query.Where(g => g.PublishDate >= now.AddYears(-2)),
					PublishingDateOptions.LastThreeYears => query.Where(g => g.PublishDate >= now.AddYears(-3)),
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
					SortingOptions.PriceAscending => query.OrderBy(g => g.Price),
					SortingOptions.PriceDescending => query.OrderByDescending(g => g.Price),
					SortingOptions.MostCommented => query.OrderByDescending(g => g.Comments.Count),
					SortingOptions.MostPopular => query.OrderByDescending(g => g.Views),
					SortingOptions.New => query.OrderByDescending(g => g.PublishDate),
					_ => query
				};
			}
			
			query = ApplyPagination(query, filter);

			return await query.ToListAsync();
		}

		private static IQueryable<Game> ApplyPagination(IQueryable<Game> query, GameFilter filter)
		{
			int pageCount = 10;

			if (!string.IsNullOrEmpty(filter.PageCount) && !filter.PageCount.Equals(PaginationPageCountOptions.All))
			{
				var parse = int.TryParse(filter.PageCount, out pageCount);

				if (!parse)
				{
					pageCount = 10;
				}
			}

			return query.Skip((filter.Page - 1) * pageCount).Take(pageCount);
		}
	}
}
