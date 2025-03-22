using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Body", "GameId", "IsDeleted", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("525d6ae7-9e7f-4514-930f-c62428c41948"), "This is a test comment 2", new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), false, "John", null },
                    { new Guid("bce96b76-0624-4248-9b34-d30655495c2d"), "This is a test comment", new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), false, "Paul", null }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "Date",
                value: new DateTime(2025, 3, 22, 18, 56, 54, 392, DateTimeKind.Local).AddTicks(9706));

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Body", "GameId", "IsDeleted", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("66d313b8-da86-4444-aae6-98f3737624eb"), "This is a test reply to comment 2", new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), false, "John", new Guid("525d6ae7-9e7f-4514-930f-c62428c41948") },
                    { new Guid("96d2c845-1144-4a6b-a8cb-abced83a8b2c"), "This is a test reply to test comment", new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), false, "Paul", new Guid("bce96b76-0624-4248-9b34-d30655495c2d") },
                    { new Guid("0db6fa93-f8a1-43f5-a57c-4405bce7aa63"), "This is a tier 3 reply to test comment", new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), false, "Paul", new Guid("96d2c845-1144-4a6b-a8cb-abced83a8b2c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GameId",
                table: "Comments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "Date",
                value: new DateTime(2025, 3, 19, 15, 6, 24, 811, DateTimeKind.Local).AddTicks(9574));
        }
    }
}
