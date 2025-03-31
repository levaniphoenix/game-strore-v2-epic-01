using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class Comment
{
	[Key]
	public Guid Id { get; set; }

	[Required]
	public string Body { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public bool IsDeleted { get; set; }

	// Recursive hierarchy
	public Guid? ParentId { get; set; }
	public Comment? Parent { get; set; }
	public ICollection<Comment> Replies { get; set; } = [];

	[Required]
	public Guid GameId { get; set; }
	public Game Game { get; set; } = null!;

	// Computed property for deleted content handling
	public string DisplayContent 
	{
		get
		{
			if (IsDeleted)
			{
				return "A comment/quote was deleted";
			}

			if (Parent != null && Parent.IsDeleted)
			{
				return "[A comment/quote was deleted]" + Body;
			}

			return Body;
		}
	}
}
