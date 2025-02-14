using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
	public class GamestoreDBContext : DbContext
	{
		public GamestoreDBContext(DbContextOptions<GamestoreDBContext> options) : base(options)
		{
		}

		public DbSet<Game> Games { get; set; } = default!;
		public DbSet<Genre> Genres { get; set; } = default!;
		public DbSet<Platform> Platforms { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Game>().HasIndex(g => g.Key).IsUnique();
			modelBuilder.Entity<Game>().HasIndex(g => g.Name).IsUnique(true);
			modelBuilder.Entity<Game>()
				.HasMany(e => e.Platforms)
				.WithMany(e => e.Games)
				.UsingEntity<GamePlatform>(
					j => j
						.HasOne(e => e.Platform)
						.WithMany()
						.HasForeignKey(e => e.PlatformId),
					j => j
						.HasOne(e => e.Game)
						.WithMany()
						.HasForeignKey(e => e.GameId)
				);

			modelBuilder.Entity<Game>()
				.HasMany(e => e.Genres)
				.WithMany(e => e.Games)
				.UsingEntity<GameGenre>(
					j => j
						.HasOne(e => e.Genre)
						.WithMany()
						.HasForeignKey(e => e.GenreId),
					j => j
						.HasOne(e => e.Game)
						.WithMany()
						.HasForeignKey(e => e.GameId)
				);

			modelBuilder.Entity<Genre>()
				.HasOne(e => e.Parent)
				.WithMany(e => e.Children)
				.HasForeignKey(e => e.ParentGenreId);

			modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique(true);

			modelBuilder.Entity<Platform>().HasIndex(p => p.Type).IsUnique(true);

			modelBuilder.Entity<GamePlatform>()
				.HasKey(gp => new { gp.GameId, gp.PlatformId });

			modelBuilder.Entity<GameGenre>()
				.HasKey(gg => new { gg.GameId, gg.GenreId });

			DBSeeder.seed(modelBuilder);
		}
	}
}
