using AutoMapper;
using Business;
using Business.Interfaces;
using Business.Services;
using Data.Data;
using Data.Interfaces;
using Gamestore.CustomDeserializer;
using Gamestore.ExeptionHandlers;
using Gamestore.Filters;
using Gamestore.Middleware;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using QuestPDF.Infrastructure;
using Serilog;

namespace Gamestore;

public class Startup(IConfiguration configuration)
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddHttpClient("PaymentClient", client =>
		{
			var baseUrl = configuration["PaymentApiBaseUrl"];
			client.BaseAddress = new Uri(baseUrl!);
			client.DefaultRequestHeaders.Add("Accept", "application/json");
		})
		.AddPolicyHandler(GetRetryPolicy());

		QuestPDF.Settings.License = LicenseType.Community;
		services.AddSerilog();
		services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new NullableGuidConverter());
			});

		var connectionString = configuration.GetConnectionString("DefaultConnection");
		services.AddDbContext<GamestoreDBContext>(options =>
			options.UseSqlServer(connectionString));

		services.AddExceptionHandler<GameStoreValidationExceptionHandler>();

		var mapperConfig = new MapperConfiguration(mc =>
		{
			mc.AddProfile(new AutomapperProfile());
		});
		IMapper mapper = mapperConfig.CreateMapper();
		services.AddSingleton(mapper);

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IGameService, GameService>();
		services.AddScoped<IPlatformService, PlatformService>();
		services.AddScoped<IGenreService, GenreService>();
		services.AddScoped<IPublisherService, PublisherService>();
		services.AddScoped<IOrderService, OrderService>();
		services.AddScoped<ICommentService, CommentService>();
		services.AddSingleton<IBannedUsersService, BannedUsersService>();

		services.AddControllers(options =>
		{
			options.Filters.Add<TotalGamesHeaderFilter>();
		});

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddCors(options =>
		{
			options.AddPolicy(
				"AllowAny",
				policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader();
				});
		});
	}

	public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors("AllowAny");
		app.UseMiddleware<RequestLoggingMiddleware>();
		app.UseExceptionHandler(_ => { });
		app.UseRouting();
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}

	private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
	{
		return HttpPolicyExtensions
			.HandleTransientHttpError()
			.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // Exponential backoff
	}
}
