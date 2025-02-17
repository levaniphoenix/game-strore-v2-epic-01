using Business.Exceptions;
using Business.Models;
using Business.Services;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using Humanizer;
using Moq;
using System.Linq.Expressions;

namespace Gamestore.Tests.BusinessTests
{
	[TestFixture]
	public class GameServiceTests
	{


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
			GameModel game = new GameModel() { Name = "Test Game", Description = "Test Game desc" };

			mockUnitOfWork.Setup(m => m.GameRepository!.GetAllAsync(It.IsAny<Expression<Func<Game, bool>>?>(), It.IsAny<Func<IQueryable<Game>, IOrderedQueryable<Game>>?>(),It.IsAny<string>()))
				.ReturnsAsync(DBSeeder.Games.Where(g => g.Name == game.Name));
					
			mockUnitOfWork.Setup(m => m.GameRepository!.AddAsync(It.IsAny<Game>()));

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

			var act = async () => await gameService.AddAsync(game);

			await act.Should().ThrowAsync<GameStoreException>().WithMessage(ErrorMessages.GameNameAlreadyExists);
		}

	}
}
