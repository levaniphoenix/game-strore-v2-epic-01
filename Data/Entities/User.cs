using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class User
{
	[Key]
	[Required]
	public Guid Id { get; set; }

	[Required]
	[Column(TypeName = "VARCHAR(100)")]
	public string FirstName { get; set; }

	[Required]
	[Column(TypeName = "VARCHAR(100)")]
	public string LastName { get; set; }

	[Required]
	[Column(TypeName = "VARCHAR(255)")]
	public string Email { get; set; }

	[Required]
	[MaxLength(2058)]
	public string PasswordHash { get; set; }

	public bool IsExternalUser { get; set; } = default!;

	public bool IsBanned { get; set; } = default!;

	public DateTime BannedUntil { get; set; } = default!;

	public virtual ICollection<Role> Roles { get; set; } = default!;
}
