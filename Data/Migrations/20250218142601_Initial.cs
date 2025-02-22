using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Games",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
					Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Games", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Genres",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
					ParentGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Genres", x => x.Id);
					table.ForeignKey(
						name: "FK_Genres_Genres_ParentGenreId",
						column: x => x.ParentGenreId,
						principalTable: "Genres",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "Platforms",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Type = table.Column<string>(type: "VARCHAR(100)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Platforms", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "GameGenres",
				columns: table => new
				{
					GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_GameGenres", x => new { x.GameId, x.GenreId });
					table.ForeignKey(
						name: "FK_GameGenres_Games_GameId",
						column: x => x.GameId,
						principalTable: "Games",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_GameGenres_Genres_GenreId",
						column: x => x.GenreId,
						principalTable: "Genres",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "GamePlatforms",
				columns: table => new
				{
					GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_GamePlatforms", x => new { x.GameId, x.PlatformId });
					table.ForeignKey(
						name: "FK_GamePlatforms_Games_GameId",
						column: x => x.GameId,
						principalTable: "Games",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_GamePlatforms_Platforms_PlatformId",
						column: x => x.PlatformId,
						principalTable: "Platforms",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "Games",
				columns: new[] { "Id", "Description", "Key", "Name" },
				values: new object[,]
				{
					{ new Guid("3d5d2761-71b7-4c58-8d0c-49c4c00aa1f6"), "This is a test game 3", "test_game_3", "Test Game 3" },
					{ new Guid("74c673ee-af55-4f16-aed9-147a93fbe2ae"), "This is a test game", "test_game", "Test Game" },
					{ new Guid("c5ee03cb-e9f5-4a52-a8a7-981698f5e228"), "This is a test game 2", "test_game_2", "Test Game 2" }
				});

			migrationBuilder.InsertData(
				table: "Genres",
				columns: new[] { "Id", "Name", "ParentGenreId" },
				values: new object[,]
				{
					{ new Guid("35f416fd-5fe3-475d-b484-0ce8b96acce1"), "Skill", null },
					{ new Guid("6856a56c-93e8-47a4-845e-f4c81b4cc7b7"), "Sports Races", null },
					{ new Guid("93eaaa3c-a24c-4ff6-aee0-e9932805b722"), "Strategy", null },
					{ new Guid("cbe02a7e-a369-4b96-9b22-badab86aab9f"), "Adventure", null },
					{ new Guid("ecee198c-3567-4e5d-a772-bcca1711b858"), "Action", null }
				});

			migrationBuilder.InsertData(
				table: "Platforms",
				columns: new[] { "Id", "Type" },
				values: new object[,]
				{
					{ new Guid("414bb869-2ea7-4107-909f-7e4b3601b1e0"), "Browser" },
					{ new Guid("8a0cb8d9-757a-4b2c-9a34-cb65d3d17d66"), "Console" },
					{ new Guid("9fa91cb5-a91b-49c9-85b3-84aec784efb8"), "Mobile" },
					{ new Guid("d4185120-0652-4e8f-9bba-2299a4508317"), "Desktop" }
				});

			migrationBuilder.InsertData(
				table: "GameGenres",
				columns: new[] { "GameId", "GenreId" },
				values: new object[,]
				{
					{ new Guid("3d5d2761-71b7-4c58-8d0c-49c4c00aa1f6"), new Guid("ecee198c-3567-4e5d-a772-bcca1711b858") },
					{ new Guid("74c673ee-af55-4f16-aed9-147a93fbe2ae"), new Guid("6856a56c-93e8-47a4-845e-f4c81b4cc7b7") },
					{ new Guid("74c673ee-af55-4f16-aed9-147a93fbe2ae"), new Guid("93eaaa3c-a24c-4ff6-aee0-e9932805b722") },
					{ new Guid("c5ee03cb-e9f5-4a52-a8a7-981698f5e228"), new Guid("6856a56c-93e8-47a4-845e-f4c81b4cc7b7") }
				});

			migrationBuilder.InsertData(
				table: "GamePlatforms",
				columns: new[] { "GameId", "PlatformId" },
				values: new object[,]
				{
					{ new Guid("3d5d2761-71b7-4c58-8d0c-49c4c00aa1f6"), new Guid("414bb869-2ea7-4107-909f-7e4b3601b1e0") },
					{ new Guid("74c673ee-af55-4f16-aed9-147a93fbe2ae"), new Guid("414bb869-2ea7-4107-909f-7e4b3601b1e0") },
					{ new Guid("74c673ee-af55-4f16-aed9-147a93fbe2ae"), new Guid("9fa91cb5-a91b-49c9-85b3-84aec784efb8") },
					{ new Guid("c5ee03cb-e9f5-4a52-a8a7-981698f5e228"), new Guid("414bb869-2ea7-4107-909f-7e4b3601b1e0") }
				});

			migrationBuilder.InsertData(
				table: "Genres",
				columns: new[] { "Id", "Name", "ParentGenreId" },
				values: new object[,]
				{
					{ new Guid("2d40e04d-83b8-425b-bfed-1c92c9b59228"), "Puzzle", new Guid("cbe02a7e-a369-4b96-9b22-badab86aab9f") },
					{ new Guid("49787733-14d7-4386-b34c-dc7df00ae9c4"), "Arcade", new Guid("6856a56c-93e8-47a4-845e-f4c81b4cc7b7") },
					{ new Guid("89fbe7c2-3fe1-4d68-8257-575314bea5a6"), "RPG", new Guid("93eaaa3c-a24c-4ff6-aee0-e9932805b722") },
					{ new Guid("8ef07862-b216-4c6c-bed1-986a0ffe0908"), "RTS", new Guid("93eaaa3c-a24c-4ff6-aee0-e9932805b722") },
					{ new Guid("97569ead-61c6-4182-8875-70fc0d261868"), "FPS", new Guid("ecee198c-3567-4e5d-a772-bcca1711b858") },
					{ new Guid("a0c078ff-edcc-4633-9ca1-acad8e851a6d"), "TBS", new Guid("93eaaa3c-a24c-4ff6-aee0-e9932805b722") },
					{ new Guid("a4039240-2da5-4f0b-b0dd-a46e7775a377"), "Off-road", new Guid("6856a56c-93e8-47a4-845e-f4c81b4cc7b7") },
					{ new Guid("b2d28ffc-78e4-44a2-9848-c9547c93110b"), "Formula", new Guid("6856a56c-93e8-47a4-845e-f4c81b4cc7b7") },
					{ new Guid("f07a75a1-4952-4237-8c52-0dc0e6689f76"), "Rally", new Guid("6856a56c-93e8-47a4-845e-f4c81b4cc7b7") },
					{ new Guid("f92b6381-4707-4173-9b4f-db050e552bc9"), "TPS", new Guid("ecee198c-3567-4e5d-a772-bcca1711b858") }
				});

			migrationBuilder.CreateIndex(
				name: "IX_GameGenres_GenreId",
				table: "GameGenres",
				column: "GenreId");

			migrationBuilder.CreateIndex(
				name: "IX_GamePlatforms_PlatformId",
				table: "GamePlatforms",
				column: "PlatformId");

			migrationBuilder.CreateIndex(
				name: "IX_Games_Key",
				table: "Games",
				column: "Key",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Games_Name",
				table: "Games",
				column: "Name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Genres_Name",
				table: "Genres",
				column: "Name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Genres_ParentGenreId",
				table: "Genres",
				column: "ParentGenreId");

			migrationBuilder.CreateIndex(
				name: "IX_Platforms_Type",
				table: "Platforms",
				column: "Type",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "GameGenres");

			migrationBuilder.DropTable(
				name: "GamePlatforms");

			migrationBuilder.DropTable(
				name: "Genres");

			migrationBuilder.DropTable(
				name: "Games");

			migrationBuilder.DropTable(
				name: "Platforms");
		}
	}
}
