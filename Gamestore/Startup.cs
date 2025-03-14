﻿using AutoMapper;
using Business;
using Business.Interfaces;
using Business.Services;
using Data.Data;
using Data.Interfaces;
using Gamestore.ExeptionHandlers;
using Gamestore.Filters;
using Gamestore.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Gamestore;

public class Startup(IConfiguration configuration)
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddSerilog();
		services.AddControllers();

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

		services.AddControllers(options =>
		{
			options.Filters.Add<TotalGamesHeaderFilter>();
		});

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
	}

	public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseMiddleware<RequestLoggingMiddleware>();
		app.UseExceptionHandler(_ => { });
		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}
