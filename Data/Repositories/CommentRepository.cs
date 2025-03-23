using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CommentRepository(GamestoreDBContext context) : GenericRepository<Comment>(context)
{
	public override async Task<Comment?> GetByIDAsync(object id)
	{
		return await Context.Comments
			.Include(c => c.Replies) // Include replies for hierarchy
			.FirstOrDefaultAsync(c => c.Id == (Guid)id);
	}

	public override void Update(Comment entityToUpdate)
	{
		// Get the original comment from the database (to track changes)
		var originalComment = Context.Comments
			.AsNoTracking() // Ensure we don't track the entity while comparing
			.FirstOrDefault(c => c.Id == entityToUpdate.Id);

		if (originalComment == null)
		{
			return;
		}

		// Check if the Body has changed
		if (originalComment.Body != entityToUpdate.Body)
		{
			// Find all quotes referencing the original body
			var quotes = Context.Comments
				.Where(c => c.Body.Contains($"[{originalComment.Body}]"))
				.ToList();

			foreach (var quote in quotes)
			{
				// Replace the old quoted part with the new body
				var oldQuotedPart = $"[{originalComment.Body}]";
				var newQuotedPart = $"[{entityToUpdate.Body}]";

				if (quote.Body.Contains(oldQuotedPart))
				{
					quote.Body = quote.Body.Replace(oldQuotedPart, newQuotedPart);
				}
			}
		}

		// Update the comment
		Context.Comments.Update(entityToUpdate);
	}

	public override void Delete(Comment entityToDelete)
	{
		entityToDelete.IsDeleted = true;

		var replies = Context.Comments
			.Where(c => c.ParentId == entityToDelete.Id)
			.ToList();

		// Modify the reply by replacing the author's name with "A comment/quote was deleted"
		foreach (var reply in replies.Where(x => x.Body.StartsWith($"[{entityToDelete.Name}],", StringComparison.InvariantCulture)))
		{
			reply.Body = reply.Body.Replace($"[{entityToDelete.Name}]", "[A comment/quote was deleted]");
		}

		var quotes = Context.Comments
			.Where(c => c.Body.Contains($"[{entityToDelete.Body}]"))
			.ToList();

		foreach (var quote in quotes)
		{
			// Modify the quote by replacing the quoted part with "A comment/quote was deleted"
			var quotedPart = $"[{entityToDelete.Body}]";
			if (quote.Body.Contains(quotedPart))
			{
				quote.Body = quote.Body.Replace(quotedPart, "[A comment/quote was deleted]");
			}
		}
	}
}
