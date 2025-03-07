using Serilog;

namespace Gamestore;
public static class Program
{
	public static void Main(string[] args)
	{
		Log.Logger = new LoggerConfiguration()
		.Filter.ByExcluding("SourceContext = 'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware'") // disable exception logs from asp.net core
		.ReadFrom.Configuration(new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build())
		.Enrich.FromLogContext()
		.CreateLogger();

		try
		{
			Log.Information("Starting the web application");
			CreateHostBuilder(args).Build().Run();
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Application startup failed!");
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
		.ConfigureWebHostDefaults(webBuilder =>
		{
			webBuilder.UseStartup<Startup>();
		});
}
