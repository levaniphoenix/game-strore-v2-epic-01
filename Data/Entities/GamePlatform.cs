﻿using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
	public class GamePlatform
	{
		[Required]
		public Guid GameId { get; set; }
		public Game Game { get; set; } = default!;
		[Required]
		public Guid PlatformId { get; set; }
		public Platform Platform { get; set; } = default!;
	}
}
