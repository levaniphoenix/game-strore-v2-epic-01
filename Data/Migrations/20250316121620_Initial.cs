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
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    HomePage = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UnitInStock = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e"), "Strategy", null },
                    { new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7"), "Sports Races", null },
                    { new Guid("970af4f3-0145-4c47-a747-51188db37650"), "Action", null },
                    { new Guid("d5bb32d9-ccdf-46b0-9859-4d9d897d39af"), "Adventure", null },
                    { new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce"), "Skill", null }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5"), "Mobile" },
                    { new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9"), "Browser" },
                    { new Guid("e8ba3b5e-b3d2-4b98-a525-3516f870d6ea"), "Desktop" },
                    { new Guid("f072a5e8-47d1-4778-a508-8350f53dc3a4"), "Console" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "Description", "HomePage" },
                values: new object[,]
                {
                    { new Guid("407fb582-9b59-4dc9-89e7-49a8a6004e20"), "Test Publisher", null, null },
                    { new Guid("4ef83756-fcfc-43d5-b3e1-b91b4a316486"), "Test Publisher 2", null, null },
                    { new Guid("82ee6a6b-2722-410b-ac63-a2f919fed6e4"), "Test Publisher 3", null, null }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Discount", "Key", "Name", "Price", "PublisherId", "UnitInStock" },
                values: new object[,]
                {
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), "This is a test game", 1, "test_game", "Test Game", 10.0, new Guid("407fb582-9b59-4dc9-89e7-49a8a6004e20"), 10 },
                    { new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), "This is a test game 2", 2, "test_game_2", "Test Game 2", 20.0, new Guid("4ef83756-fcfc-43d5-b3e1-b91b4a316486"), 20 },
                    { new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), "This is a test game 3", 3, "test_game_3", "Test Game 3", 30.0, new Guid("82ee6a6b-2722-410b-ac63-a2f919fed6e4"), 30 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("217165cd-1739-4a1c-a437-f768ef8bb0c7"), "RTS", new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") },
                    { new Guid("387eda52-897b-4e2f-94d7-38c5620a3513"), "TBS", new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") },
                    { new Guid("6344f3d4-78d1-49a9-b328-17384f8c8003"), "RPG", new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") },
                    { new Guid("67435def-e5d4-45df-85db-774d03f7c88d"), "Rally", new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
                    { new Guid("818f2abb-57ae-42d5-aab1-231a5daf5c70"), "Arcade", new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
                    { new Guid("93a10982-1873-4e84-ae7e-54c62cf6e2d2"), "Formula", new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
                    { new Guid("958b7723-fb54-4016-badb-86351ef95a16"), "Off-road", new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
                    { new Guid("b0e9e6ce-1596-4dd2-a880-0257693e8b7e"), "FPS", new Guid("970af4f3-0145-4c47-a747-51188db37650") },
                    { new Guid("d40645c6-697a-4444-b205-206047d77b18"), "TPS", new Guid("970af4f3-0145-4c47-a747-51188db37650") },
                    { new Guid("d7359da5-b867-4b1a-962c-dda749590342"), "Puzzle", new Guid("d5bb32d9-ccdf-46b0-9859-4d9d897d39af") }
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "GameId", "GenreId" },
                values: new object[,]
                {
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") },
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
                    { new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
                    { new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), new Guid("970af4f3-0145-4c47-a747-51188db37650") }
                });

            migrationBuilder.InsertData(
                table: "GamePlatforms",
                columns: new[] { "GameId", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5") },
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9") },
                    { new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9") },
                    { new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9") }
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
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_CompanyName",
                table: "Publishers",
                column: "CompanyName",
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

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
