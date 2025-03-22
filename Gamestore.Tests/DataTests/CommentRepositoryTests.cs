using Data.Data;
using Data.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests.DataTests;

[TestFixture]
public class CommentRepositoryTests
{
	private GamestoreDBContext context;

	[SetUp]
	public void Setup()
	{
		context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
		context.Database.OpenConnection();
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();
	}

	[TearDown]
	public void Teardown()
	{
		context.Database.CloseConnection();
		context.Dispose();
	}

	[Test]
	public async Task GetAllReturnsAllValues()
	{
		var unitOfWork = new UnitOfWork(context);
		var expected = DBSeeder.Comments.Concat(DBSeeder.Replies).Concat(DBSeeder.Tier3Replies).OrderByDescending(x => x.Id);

		var result = await unitOfWork.CommentRepository.GetAllAsync(orderBy: x => x.OrderByDescending(x => x.Id));

		result.Should().BeEquivalentTo(expected, options =>
		{
			options.AllowingInfiniteRecursion();
			options.Excluding(c => c.Replies).Excluding(c => c.Parent);
			return options;
		});
	}

	[Test]
	public async Task GetByIDReturnsSingleValue()
	{
		var unitOfWork = new UnitOfWork(context);
		var expected = DBSeeder.Comments[0];
		var result = await unitOfWork.CommentRepository.GetByIDAsync(expected.Id);
		result.Should().BeEquivalentTo(expected, options => options
			.Excluding(c => c.Replies));
	}

	[Test]
	public async Task Delete_ShouldModifyRepliesAndQuotesCorrectly()
	{
		var unitOfWork = new UnitOfWork(context);

		// Create a top-level comment
		var topLevelComment = new Comment
		{
			Id = Guid.NewGuid(),
			Name = "Author1",
			Body = "This is a top-level comment.",
			GameId = DBSeeder.Games[0].Id
		};

		// Create a reply to the top-level comment
		var reply = new Comment
		{
			Id = Guid.NewGuid(),
			Name = "Author2",
			Body = "[Author1], This is a reply.",
			ParentId = topLevelComment.Id,
			GameId = topLevelComment.GameId
		};

		// Create a quote referencing the top-level comment
		var quote = new Comment
		{
			Id = Guid.NewGuid(),
			Name = "Author3",
			Body = "[This is a top-level comment.], This is a quote.",
			ParentId = topLevelComment.Id,
			GameId = topLevelComment.GameId
		};

		// Add comments to the in-memory database
		await context.Comments.AddRangeAsync(topLevelComment, reply, quote);
		await unitOfWork.SaveAsync();

		// Act
		unitOfWork.CommentRepository.Delete(topLevelComment);

		// Assert
		// Reload data from the database
		var deletedComment = await context.Comments.FindAsync(topLevelComment.Id);
		var updatedReply = await context.Comments.FindAsync(reply.Id);
		var updatedQuote = await context.Comments.FindAsync(quote.Id);

		// Assert the deleted comment
		deletedComment.Should().NotBeNull();
		deletedComment.IsDeleted.Should().BeTrue();
		deletedComment.DisplayContent.Should().Be("A comment/quote was deleted");

		// Assert the reply
		updatedReply.Should().NotBeNull();
		updatedReply.Body.Should().Be("[A comment/quote was deleted], This is a reply.");

		// Assert the quote
		updatedQuote.Should().NotBeNull();
		updatedQuote.Body.Should().Be("[A comment/quote was deleted], This is a quote.");
	}

	[Test]
	public async Task UpdateAsync_ShouldUpdateQuotesCorrectly()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);

		// Create a top-level comment
		var topLevelComment = new Comment
		{
			Id = Guid.NewGuid(),
			Name = "Author1",
			Body = "This is a top-level comment.",
			GameId = DBSeeder.Games[0].Id
		};

		// Create a quote referencing the top-level comment
		var quote = new Comment
		{
			Id = Guid.NewGuid(),
			Name = "Author2",
			Body = "[This is a top-level comment.], This is a quote.",
			ParentId = topLevelComment.Id,
			GameId = topLevelComment.GameId
		};

		// Add comments to the in-memory database
		await context.Comments.AddRangeAsync(topLevelComment, quote);
		await unitOfWork.SaveAsync();

		// Act
		topLevelComment.Body = "This is the updated comment.";
		unitOfWork.CommentRepository.Update(topLevelComment);

		// Assert
		// Reload the quote from the database
		var updatedQuote = await unitOfWork.CommentRepository.GetByIDAsync(quote.Id);

		// Verify the quote body is updated with the new comment body
		updatedQuote.Should().NotBeNull();
		updatedQuote.Body.Should().Be("[This is the updated comment.], This is a quote.");
	}
}
