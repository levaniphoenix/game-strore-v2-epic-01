using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Data;
using FluentAssertions;
using Gamestore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Business.Models.GenreModel;

namespace Gamestore.Tests.ApiTests;
[TestFixture]
public class GenresControllerTests
{
	private readonly IMapper mapper = UnitTestHelper.CreateMapperProfile();

	[Test]
	public async Task GetShouldReturnOk()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService
			.Setup(s => s.GetAllAsync())
			.ReturnsAsync(mapper.Map<IEnumerable<GenreModel>> (DBSeeder.Genres));
		var genresController = new GenresController(mockGenreService.Object);

		var result = await genresController.Get();

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<GenreDetails>)okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGenres.Should().NotBeNull();
		returnedGenres.Count().Should().Be(DBSeeder.Genres.Length);
	}

	[Test]
	public async Task GetByIdShouldReturnOk()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService
			.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(
			(Guid id) =>
			{
				return mapper.Map<GenreModel>(DBSeeder.Genres.SingleOrDefault(g => g.Id == id));
			}
			);
		
		var genresController = new GenresController(mockGenreService.Object);

		var id = DBSeeder.Genres[0].Id;
		var result = await genresController.Get(id);

		var okResult = result.Result as OkObjectResult;
		var returnedGenre = (GenreDetails?)okResult?.Value;

		result.Result.Should().BeOfType<OkObjectResult>();
		returnedGenre.Should().NotBeNull();
	}

	[Test]
	public async Task GetByIdShouldReturnNotFound()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService
			.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(
			(Guid id) =>
			{
				return mapper.Map<GenreModel>(DBSeeder.Genres.SingleOrDefault(g => g.Id == id));
			}
			);

		var genresController = new GenresController(mockGenreService.Object);

		var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
		var result = await genresController.Get(id);

		result.Result.Should().BeOfType<NotFoundObjectResult>();
	}

	[Test]
	public async Task GetGamesByGenreShouldReturnOk()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService
			.Setup(s => s.GetGamesByGenreIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(
			(Guid gameId) =>
			{
				var games = (from gameGenre in DBSeeder.GameGenres
							 join genre in DBSeeder.Genres on gameGenre.GenreId equals genre.Id
							 join game in DBSeeder.Games on gameId equals game.Id
							 where gameGenre.GameId == game.Id
							 select game).ToList();
				return mapper.Map<IEnumerable<GameModel?>>(games);
			}
			);

		var genresController = new GenresController(mockGenreService.Object);

		var id = DBSeeder.Genres[0].Id;
		var result = await genresController.GetGamesByGenre(id);

		var okResult = result.Result as OkObjectResult;
		var returnedGames = (IEnumerable<GameDetails?>)okResult?.Value;

		result.Result.Should().BeOfType<OkObjectResult>();
		returnedGames.Should().NotBeNull();
	}

	[Test]
	public async Task GetGenresByParentIdShouldReturnOk()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService
			.Setup(s => s.GetGenresByParentId(It.IsAny<Guid>()))
			.ReturnsAsync(
			(Guid id) =>
			{
				return mapper.Map<IEnumerable<GenreModel?>>(DBSeeder.Genres.Where(g => g.ParentGenreId == id).ToList());
			}
			);

		var genresController = new GenresController(mockGenreService.Object);

		var id = DBSeeder.Genres[0].Id;
		var result = await genresController.GetGenresByParentId(id);

		var okResult = result.Result as OkObjectResult;
		var returnedGenre = (IEnumerable<GenreDetails?>)okResult?.Value;

		result.Result.Should().BeOfType<OkObjectResult>();
		returnedGenre.Should().NotBeNull();
	}

	[Test]
	public async Task GetGenresByParentIdShouldReturnNotFound()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService
			.Setup(s => s.GetGenresByParentId(It.IsAny<Guid>()))
			.ReturnsAsync(
			(Guid id) =>
			{
				return mapper.Map<IEnumerable<GenreModel?>>(DBSeeder.Genres.Where(g => g.ParentGenreId == id).ToList());
			}
			);

		var genresController = new GenresController(mockGenreService.Object);

		var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
		var result = await genresController.GetGenresByParentId(id);

		result.Result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task PostShouldReturnOk()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService.Setup(s => s.AddAsync(It.IsAny<GenreModel>()));
		var genresController = new GenresController(mockGenreService.Object);

		var genre = new GenreModel() { Genre = new GenreDetails() { Name ="a new genre" } };

		var result = await genresController.Post(genre);

		mockGenreService.Verify(x => x.AddAsync(It.Is<GenreModel>(g => g.Genre.Name == genre.Genre.Name)), Times.Once);
		result.Should().BeOfType<CreatedResult>();
	}

	[Test]
	public async Task PutShouldReturnOk()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService.Setup(s => s.UpdateAsync(It.IsAny<GenreModel>()));
		var genresController = new GenresController(mockGenreService.Object);

		var genre = new GenreModel() { Genre = new GenreDetails() { Name = "a new genre" } };

		var result = await genresController.Put(genre);

		mockGenreService.Verify(x => x.UpdateAsync(It.Is<GenreModel>(g => g.Genre.Name == genre.Genre.Name)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task DeleteShouldReturnOk()
	{
		var mockGenreService = new Mock<IGenreService>();
		mockGenreService.Setup(s => s.DeleteAsync(It.IsAny<GenreModel>()));
		var genresController = new GenresController(mockGenreService.Object);

		var id = Guid.Parse("00000000-0000-0000-0000-000000000001");

		var result = await genresController.Delete(id);

		mockGenreService.Verify(x => x.DeleteAsync(It.Is<Guid>(g => g == id)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}
}
