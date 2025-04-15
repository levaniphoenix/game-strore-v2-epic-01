namespace Business.Models;

public class CommentModel
{
	public CommentDetails Comment { get; set; }

	public Guid? ParentId { get; set; }

	public string? Action { get; set; }

	public Guid? GameId { get; set; }

	public class CommentDetails
	{
		public Guid? Id { get; set; } = default!;

		public string Name { get; set; }

		public string Body { get; set; }

		public ICollection<CommentDetails> ChildComments { get; set; } = [];
	}
}
