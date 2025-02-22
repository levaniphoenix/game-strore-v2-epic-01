using System.Linq.Expressions;
using AutoMapper;
using Business;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Gamestore.Tests
{
	internal static class UnitTestHelper
	{
		public static DbContextOptions<GamestoreDBContext> GetUnitTestDbOptions()
		{
			//var options = new DbContextOptionsBuilder<GamestoreDBContext>()
			//	.UseInMemoryDatabase(Guid.NewGuid().ToString())
			//	.Options

			var options = new DbContextOptionsBuilder<GamestoreDBContext>()
				.UseSqlite("Data Source=:memory:")
				.Options;

			return options;
		}

		public static IMapper CreateMapperProfile()
		{
			var myProfile = new AutomapperProfile();
			var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

			return new Mapper(configuration);
		}

		public static void SetUpMockGameRepository(Mock<IUnitOfWork> mock, Game[] data)
		{
			mock.Setup(m => m.GameRepository!.GetAllAsync(It.IsAny<Expression<Func<Game, bool>>?>(), It.IsAny<Func<IQueryable<Game>, IOrderedQueryable<Game>>?>(), It.IsAny<string>()))
				.ReturnsAsync((Expression<Func<Game, bool>>? filter,
					   Func<IQueryable<Game>, IOrderedQueryable<Game>>? orderBy,
					   string includeProperties) =>
				{
					IQueryable<Game> query = data.AsQueryable();

					if (filter != null)
					{
						query = query.Where(filter);
					}

					foreach (var includeProperty in includeProperties.Split
						(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					{
						query = query.Include(includeProperty);
					}

					if (orderBy != null)
					{
						return orderBy(query).ToList();
					}
					else
					{
						return query.ToList();
					}

				});
		}

		public static void SetUpMockGenreRepository(Mock<IUnitOfWork> mock, Genre[] data)
		{
			mock.Setup(m => m.GenreRepository!.GetAllAsync(It.IsAny<Expression<Func<Genre, bool>>?>(), It.IsAny<Func<IQueryable<Genre>, IOrderedQueryable<Genre>>?>(), It.IsAny<string>()))
				.ReturnsAsync((Expression<Func<Genre, bool>>? filter,
					   Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy,
					   string includeProperties) =>
				{
					IQueryable<Genre> query = data.AsQueryable();

					if (filter != null)
					{
						query = query.Where(filter);
					}

					foreach (var includeProperty in includeProperties.Split
						(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					{
						query = query.Include(includeProperty);
					}

					if (orderBy != null)
					{
						return orderBy(query).ToList();
					}
					else
					{
						return query.ToList();
					}

				});
		}

		public static void SetUpMockPlatformRepository(Mock<IUnitOfWork> mock, Platform[] data)
		{
			mock.Setup(m => m.PlatformRepository!.GetAllAsync(It.IsAny<Expression<Func<Platform, bool>>?>(), It.IsAny<Func<IQueryable<Platform>, IOrderedQueryable<Platform>>?>(), It.IsAny<string>()))
				.ReturnsAsync((Expression<Func<Platform, bool>>? filter,
					   Func<IQueryable<Platform>, IOrderedQueryable<Platform>>? orderBy,
					   string includeProperties) =>
				{
					IQueryable<Platform> query = data.AsQueryable();

					if (filter != null)
					{
						query = query.Where(filter);
					}

					foreach (var includeProperty in includeProperties.Split
						(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					{
						query = query.Include(includeProperty);
					}

					if (orderBy != null)
					{
						return orderBy(query).ToList();
					}
					else
					{
						return query.ToList();
					}

				});
		}
	}
}