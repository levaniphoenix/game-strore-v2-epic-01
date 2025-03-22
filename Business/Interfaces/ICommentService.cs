using Business.Models;

namespace Business.Interfaces;

public interface ICommentService : ICrud<CommentModel>
{
	Task<IEnumerable<CommentModel>> GetCommentsByGameKeyAsync(string key);

	Task ReplyToCommentAsync(Guid? parentCommentId, string name, string replyBody);

	Task QuoteCommentAsync(Guid? parentCommentId, string name, string quoteBody);
}
