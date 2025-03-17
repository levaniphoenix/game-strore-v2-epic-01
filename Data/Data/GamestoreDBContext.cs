using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
	public class GamestoreDBContext : DbContext
	{
		public DbSet<Game> Games { get; set; } = default!;
		public DbSet<Genre> Genres { get; set; } = default!;
		public DbSet<Platform> Platforms { get; set; } = default!;
		public DbSet<Publisher> Publishers { get; set; } = default!;
		public DbSet<Order> Orders { get; set; } = default!;

		public DbSet<GamePlatform> GamePlatforms { get; set; } = default!;
		public DbSet<GameGenre> GameGenres { get; set; } = default!;
		public DbSet<OrderGame> OrderDetails { get; set; } = default!;

		public GamestoreDBContext(DbContextOptions<GamestoreDBContext> options) : base(options)
		{
		}

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

			modelBuilder.Entity<OrderGame>()
				.HasOne(og => og.Order)
				.WithMany(o => o.OrderDetails)
				.HasForeignKey(og => og.OrderId);

			modelBuilder.Entity<OrderGame>()
				.HasOne(og => og.Product)
				.WithMany(g => g.OrderGames)
				.HasForeignKey(og => og.ProductId);

			modelBuilder.Entity<Genre>()
				.HasOne(e => e.Parent)
				.WithMany(e => e.Children)
				.HasForeignKey(e => e.ParentGenreId);

			modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique(true);

			modelBuilder.Entity<Platform>().HasIndex(p => p.Type).IsUnique(true);

			modelBuilder.Entity<Publisher>().HasIndex(p => p.CompanyName).IsUnique(true);

			modelBuilder.Entity<GamePlatform>()
				.HasKey(gp => new { gp.GameId, gp.PlatformId });

			modelBuilder.Entity<GameGenre>()
				.HasKey(gg => new { gg.GameId, gg.GenreId });

			modelBuilder.Entity<OrderGame>()
				.HasKey(og => new { og.OrderId, og.ProductId });

			DBSeeder.Seed(modelBuilder);
		}
	}
}
