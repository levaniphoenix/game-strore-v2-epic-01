using System;
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
                name: "GameGenre",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GamesId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatforms",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlatformsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatforms", x => new { x.GamesId, x.PlatformsId });
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Platforms_PlatformsId",
                        column: x => x.PlatformsId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("056341d2-838e-40f3-b3ea-6c64c50922d2"), "Strategy", null },
                    { new Guid("81cbf5ee-fb01-412a-97cd-79bf741b5220"), "Action", null },
                    { new Guid("d4420df2-0630-4783-a203-2ff20a0bb388"), "Adventure", null },
                    { new Guid("d4cfd6a1-5fe8-4c13-a22f-ed34169003fa"), "Sports Races", null },
                    { new Guid("e5a464d8-2bfc-41bc-8bba-e2efbc028231"), "Skill", null }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("5d20764d-94a4-4c8b-b2e3-abef4cd84b2c"), "Console" },
                    { new Guid("64d99402-5850-42b1-ba04-659c2c56afa2"), "Desktop" },
                    { new Guid("a6b26716-5b71-46f7-b2f8-a19c0dd573c6"), "Mobile" },
                    { new Guid("fd1daa9a-6e5b-4d16-9b4e-9890f6247fdf"), "Browser" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("1b433802-d162-4a34-92f0-1cedc54f5788"), "RPG", new Guid("056341d2-838e-40f3-b3ea-6c64c50922d2") },
                    { new Guid("28e19936-9787-4ee9-8361-d1698515563a"), "Formula", new Guid("d4cfd6a1-5fe8-4c13-a22f-ed34169003fa") },
                    { new Guid("3d3179bd-c77b-4d04-8e9b-5e38186ca7e7"), "FPS", new Guid("81cbf5ee-fb01-412a-97cd-79bf741b5220") },
                    { new Guid("44d96f8e-7ae8-4f21-a836-8d2f018da989"), "RTS", new Guid("056341d2-838e-40f3-b3ea-6c64c50922d2") },
                    { new Guid("57f45cec-c522-4c29-a802-ee93f33b2cff"), "TPS", new Guid("81cbf5ee-fb01-412a-97cd-79bf741b5220") },
                    { new Guid("7379c31b-3085-43af-afee-d004f19df8b1"), "Off-road", new Guid("d4cfd6a1-5fe8-4c13-a22f-ed34169003fa") },
                    { new Guid("74db4762-d154-4d64-97d4-33ae1518fe83"), "TBS", new Guid("056341d2-838e-40f3-b3ea-6c64c50922d2") },
                    { new Guid("8669663c-4678-40e9-8f37-ddd4465a9cf9"), "Arcade", new Guid("d4cfd6a1-5fe8-4c13-a22f-ed34169003fa") },
                    { new Guid("a3877603-c23a-410b-8193-d6acc78b98d1"), "Puzzle", new Guid("d4420df2-0630-4783-a203-2ff20a0bb388") },
                    { new Guid("a448d817-2740-4a88-a36a-f7e3f9aa1ce1"), "Rally", new Guid("d4cfd6a1-5fe8-4c13-a22f-ed34169003fa") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenresId",
                table: "GameGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformsId",
                table: "GamePlatforms",
                column: "PlatformsId");

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
                name: "GameGenre");

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
