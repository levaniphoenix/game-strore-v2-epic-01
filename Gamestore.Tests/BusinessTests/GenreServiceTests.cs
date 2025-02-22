using Business.Exceptions;
using Business.Models;
using Business.Services;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using Moq;
using static Business.Models.GenreModel;

namespace Gamestore.Tests.BusinessTests
{
	[TestFixture]
	public class GenreServiceTests
	{
		[Test]
		public async Task GenreServiceAddAsyncAddsToDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.GenreRepository!.AddAsync(It.IsAny<Genre>()));
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);

			var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

			await genreService.AddAsync(new GenreModel { Genre = new GenreDetails { Name = "VR" } });

			mockUnitOfWork.Verify(u => u.GenreRepository!.AddAsync(It.IsAny<Genre>()), Times.Once);

			mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Test]
		public void GenreServiceAddAsyncThrowsExceptionOnDuplicateName()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.GenreRepository!.AddAsync(It.IsAny<Genre>()));
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			Assert.ThrowsAsync<GameStoreValidationException>(() => genreService.AddAsync(new GenreModel { Genre = new GenreDetails { Name = "Action" } }));
		}

		[Test]
		public async Task GenreServiceDeleteAsyncDeletesFromDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.GenreRepository!.DeleteByIdAsync(It.IsAny<object>()));
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

			var id = DBSeeder.Genres[0].Id;

			await genreService.DeleteAsync(id);
			mockUnitOfWork.Verify(u => u.GenreRepository!.DeleteByIdAsync(It.IsAny<object>()), Times.Once);
			mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Test]
		public async Task GenreServiceGetAllAsyncReturnsGenres()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var mapper = UnitTestHelper.CreateMapperProfile();
			var genreService = new GenreService(mockUnitOfWork.Object, mapper);
			var genres = await genreService.GetAllAsync();
			var expected = mapper.Map<IEnumerable<GenreModel>>(DBSeeder.Genres);
			genres.Should().BeEquivalentTo(expected);
		}

		[Test]
		public async Task GenreServiceGetGenresByParentIdReturnsChildren()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var mapper = UnitTestHelper.CreateMapperProfile();
			var genreService = new GenreService(mockUnitOfWork.Object, mapper);
			var genres = await genreService.GetGenresByParentId(DBSeeder.Genres[0].Id);
			var expected = mapper.Map<IEnumerable<GenreModel?>>(DBSeeder.Genres.Where(g => g.ParentGenreId == DBSeeder.Genres[0].Id));
			genres.Should().NotBeEmpty();
			genres.Should().BeEquivalentTo(expected);
		}

		[Test]
		public async Task GenreServiceGetGenresByParentIdReturnsNullForNonExistentParent()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var mapper = UnitTestHelper.CreateMapperProfile();
			var genreService = new GenreService(mockUnitOfWork.Object, mapper);
			var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var genres = await genreService.GetGenresByParentId(id);
			genres.Should().BeEmpty();
		}

		[Test]
		public async Task GenreServiceGetByIdAsyncReturnsGenre()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();

			mockUnitOfWork.Setup(m => m.GenreRepository!.GetByIDAsync(It.IsAny<object>()))
				.ReturnsAsync((object id) => DBSeeder.Genres.SingleOrDefault(g => g.Id == (Guid)id));

			var mapper = UnitTestHelper.CreateMapperProfile();
			var genreService = new GenreService(mockUnitOfWork.Object, mapper);
			var genre = await genreService.GetByIdAsync(DBSeeder.Genres[0].Id);
			var expected = mapper.Map<GenreModel?>(DBSeeder.Genres[0]);
			genre.Should().BeEquivalentTo(expected);
		}

		[Test]
		public async Task GenreServiceGetByIdAsyncReturnsNull()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var mapper = UnitTestHelper.CreateMapperProfile();
			var genreService = new GenreService(mockUnitOfWork.Object, mapper);
			var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var genre = await genreService.GetByIdAsync(id);
			genre.Should().BeNull();
		}

		[Test]
		public async Task GenreServiceUpdateAsyncUpdatesDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.GenreRepository!.Update(It.IsAny<Genre>()));
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			var genre = new GenreModel { Genre = new GenreDetails { Id = DBSeeder.Genres[0].Id, Name = "VR" } };
			await genreService.UpdateAsync(genre);
			mockUnitOfWork.Verify(u => u.GenreRepository!.Update(It.IsAny<Genre>()), Times.Once);
			mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Test]
		public void GenreServiceUpdateAsyncUpdateThrowsExceptionOnDuplicateName()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.GenreRepository!.Update(It.IsAny<Genre>()));
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			var genre = new GenreModel { Genre = new GenreDetails { Id = DBSeeder.Genres[0].Id, Name = "Action" } };
			Assert.ThrowsAsync<GameStoreValidationException>(() => genreService.UpdateAsync(genre));
		}
	}
}
