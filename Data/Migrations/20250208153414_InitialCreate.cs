using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GameId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_GamePlatform_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("000a57c3-3871-483c-8bd1-7e04d59fba81"), "Strategy", null },
                    { new Guid("00b5aa22-fca4-458c-b2fe-0ca9c41d111a"), "RPG", new Guid("000a57c3-3871-483c-8bd1-7e04d59fba81") },
                    { new Guid("0703e73d-b472-455c-9f00-447656157658"), "Off-road", new Guid("63499d64-2baf-4b14-a56b-f5065bab8173") },
                    { new Guid("13e1dbfe-c785-4e16-84aa-3f0a3b8a7cf0"), "RTS", new Guid("000a57c3-3871-483c-8bd1-7e04d59fba81") },
                    { new Guid("212673fe-09d4-4277-b0ad-2ee9f1e36408"), "Puzzle", new Guid("876808bc-5a87-464f-8210-cf922a517505") },
                    { new Guid("27ac08bb-d4ff-43cb-be3a-1ce734803b00"), "TPS", new Guid("2dcac868-4939-4370-907a-1cfb78194a2c") },
                    { new Guid("2dcac868-4939-4370-907a-1cfb78194a2c"), "Action", null },
                    { new Guid("485fd6ed-83b7-4e68-a764-bfe3b11b09de"), "Formula", new Guid("63499d64-2baf-4b14-a56b-f5065bab8173") },
                    { new Guid("59842c6a-03c1-474c-b6d7-0887d4f4a847"), "FPS", new Guid("2dcac868-4939-4370-907a-1cfb78194a2c") },
                    { new Guid("63499d64-2baf-4b14-a56b-f5065bab8173"), "Sports Races", null },
                    { new Guid("86ee6cf7-5ac3-4e1a-abba-d9e491e4c8a7"), "TBS", new Guid("000a57c3-3871-483c-8bd1-7e04d59fba81") },
                    { new Guid("876808bc-5a87-464f-8210-cf922a517505"), "Adventure", null },
                    { new Guid("9af09bc8-14ec-4278-9a63-550020252018"), "Skill", null },
                    { new Guid("9e2864a4-de70-4edc-855a-7b0cac706d8d"), "Rally", new Guid("63499d64-2baf-4b14-a56b-f5065bab8173") },
                    { new Guid("bba0aa30-9568-4320-b4f5-bdec42cd37ba"), "Arcade", new Guid("63499d64-2baf-4b14-a56b-f5065bab8173") }
                });

            migrationBuilder.InsertData(
                table: "Platform",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("71115c91-409c-433c-8093-b0284d5620a8"), "Browser" },
                    { new Guid("a41ab438-23d9-4523-9691-94db7efc95e6"), "Desktop" },
                    { new Guid("ad559831-3ac5-43ed-b4aa-8cddc790bc98"), "Console" },
                    { new Guid("bc3da320-db42-4b46-9d92-5c59c6cea5d4"), "Mobile" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformId",
                table: "GamePlatform",
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
                name: "IX_Genre_Name",
                table: "Genre",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platform_Type",
                table: "Platform",
                column: "Type",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Platform");
        }
    }
}
