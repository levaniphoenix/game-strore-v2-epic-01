using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class Publisher
{
	[Key]
	[Required]
	public Guid Id { get; set; }

	[Required]
	//unique
	[Column(TypeName = "VARCHAR(100)")]
	public string CompanyName { get; set; } = default!;

	[Column(TypeName = "VARCHAR(1000)")]
	public string? HomePage { get; set; }

	[Column(TypeName = "VARCHAR(5000)")]
	public string? Description { get; set; }

	public ICollection<Game> Games { get; set; } = default!;
}
