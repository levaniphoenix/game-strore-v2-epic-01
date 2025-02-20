using Business.Exceptions;
using Business.Models;
using Business.Services;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gamestore.Tests.BusinessTests
{
	[TestFixture]
	public class GameServiceTests
	{
		[Test]
		public async Task GameSercviceGetAllAsyncReturnsGamesFromDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.GetAllAsync(It.IsAny<Expression<Func<Game, bool>>?>(), It.IsAny<Func<IQueryable<Game>, IOrderedQueryable<Game>>?>(), It.IsAny<string>()))
				.ReturnsAsync(DBSeeder.Games);

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

			var actual = await gameService.GetAllAsync();

			actual.Should().NotBeNullOrEmpty();

		}

		[Test]
		public async Task GameSercviceAddAsyncAddsGameToDB()
		{
			var game = new GameModel() { Game = new GameDetails { Name = "a new Game", Description = "Test Game desc", }, GenreIds = [DBSeeder.Genres[0].Id], PlatformIds = [DBSeeder.Platforms[0].Id, DBSeeder.Platforms[1].Id] };

			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.AddAsync(It.IsAny<Game>()));
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			
			await gameService.AddAsync(game);
			
			mockUnitOfWork.Verify(x => x.GameRepository!.AddAsync(It.Is<Game>(x =>
				x.Name == game.Game.Name && x.Description == game.Game.Description)),Times.Once);
			mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
		}

		[Test]
		public async Task GameServiceAddAsyncThrowsArgumentNullException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.AddAsync(It.IsAny<Game>()));

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

			GameModel? game = null;

			var act = async () => await gameService.AddAsync(game!);

			await act.Should().ThrowAsync<ArgumentNullException>();
		}

		[Test]
		public async Task GameServiceAddAsyncThrowsGameStoreExeptionWithDuplicateName()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.AddAsync(It.IsAny<Game>()));
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			
			GameModel game = new GameModel() { Game = new GameDetails { Name = "Test Game", Description = "Test Game desc" } };		

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

			var act = async () => await gameService.AddAsync(game);

			await act.Should().ThrowAsync<GameStoreException>().WithMessage(ErrorMessages.GameNameAlreadyExists);
		}
	}
}
