using Data.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Gamestore.Tests.IntegrationTests
{
	internal sealed class CustomWebApplicationFactory : WebApplicationFactory<Startup>
	{
		private readonly MsSqlContainer msSqlContainer = new MsSqlBuilder()
			.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
			.WithCleanUp(true)
			.Build();

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				RemoveLibraryDbContextRegistration(services);

				//var connectionString = "Server=(localdb)\\mssqllocaldb;Database=INTEGRATIONTEST_GamestoreDB;Trusted_Connection=True;MultipleActiveResultSets=true"
				var connectionString = new SqlConnectionStringBuilder(msSqlContainer.GetConnectionString())
				{
					InitialCatalog = Guid.NewGuid().ToString("D")
				};

				services.AddDbContext<GamestoreDBContext>(options =>
					options.UseSqlServer(connectionString.ToString()));

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

		public async Task StartContainerAsync()
		{
			await msSqlContainer.StartAsync();
		}

		public async Task StopContainerAsync()
		{
			await msSqlContainer.StopAsync();
		}
	}
}
