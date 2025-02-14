using Data.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.Tests.IntegrationTests
{
	internal class CustomWebApplicationFactory : WebApplicationFactory<Startup>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				RemoveLibraryDbContextRegistration(services);

				var serviceProvider = GetInMemoryServiceProvider();

				services.AddDbContextPool<GamestoreDBContext>(options =>
				{
					options.UseInMemoryDatabase(Guid.Empty.ToString());
					options.UseInternalServiceProvider(serviceProvider);
				});

				using (var scope = services.BuildServiceProvider().CreateScope())
				{
					var context = scope.ServiceProvider.GetRequiredService<GamestoreDBContext>();

					UnitTestHelper.SeedData(context);
				}
			});
		}

		private static ServiceProvider GetInMemoryServiceProvider()
		{
			return new ServiceCollection()
				.AddEntityFrameworkInMemoryDatabase()
				.BuildServiceProvider();
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
