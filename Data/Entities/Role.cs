using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class Role
{
	[Key]
	public Guid Id { get; set; }

	[Required]
	[Column(TypeName = "VARCHAR(100)")]
	public string Name { get; set; } = default!;

	[Required]
	[Column(TypeName = "VARCHAR(255)")]
	public string Description { get; set; } = default!;

	public virtual ICollection<User> Users { get; set; } = default!;
}
