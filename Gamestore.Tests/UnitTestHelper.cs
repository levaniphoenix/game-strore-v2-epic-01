using AutoMapper;
using Business;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests
{
	internal static class UnitTestHelper
	{		
		public static DbContextOptions<GamestoreDBContext> GetUnitTestDbOptions()
		{
			//var options = new DbContextOptionsBuilder<GamestoreDBContext>()
			//	.UseInMemoryDatabase(Guid.NewGuid().ToString())
			//	.Options;

			var options = new DbContextOptionsBuilder<GamestoreDBContext>()
				.UseSqlite("Data Source=:memory:")
				.Options;

			return options;
		}

		public static void SeedData(GamestoreDBContext context)
		{
			context.SaveChanges();
		}

		public static IMapper CreateMapperProfile()
		{
			var myProfile = new AutomapperProfile();
			var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

			return new Mapper(configuration);
		}
	}
}