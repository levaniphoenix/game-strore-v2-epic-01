using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class GenreModel
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public String Name { get; set; } = default!;

		public Guid? ParentGenreId { get; set; }

		public virtual Genre Parent { get; set; } = default!;

		public virtual ICollection<Genre> Children { get; set; } = new List<Genre>();

		public ICollection<Game> Games { get; set; } = new List<Game>();
	}
}
