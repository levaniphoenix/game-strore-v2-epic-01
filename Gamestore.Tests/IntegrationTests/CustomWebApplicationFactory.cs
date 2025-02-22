using Data.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.Tests.IntegrationTests
{
	internal sealed class CustomWebApplicationFactory : WebApplicationFactory<Startup>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				RemoveLibraryDbContextRegistration(services);

				var connectionString = "Server=(localdb)\\mssqllocaldb;Database=INTEGRATIONTEST_GamestoreDB;Trusted_Connection=True;MultipleActiveResultSets=true";
				services.AddDbContext<GamestoreDBContext>(options =>
					options.UseSqlServer(connectionString));

				using var scope = services.BuildServiceProvider().CreateScope();
				var scopedServices = scope.ServiceProvider;
				var db = scopedServices.GetRequiredService<GamestoreDBContext>();
				db.Database.EnsureDeleted();
				db.Database.EnsureCreated();

			});
		}

		private static void RemoveLibraryDbContextRegistration(IServiceCollection services)
		{
			var descriptor = services.SingleOrDefault(
				d => d.ServiceType ==
					 typeof(DbContextOptions<GamestoreDBContext>));

			if (descriptor != null)
			{
				services.Remove(descriptor);
			}
		}
	}
}
