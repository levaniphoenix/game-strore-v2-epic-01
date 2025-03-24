using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPublishDateAndViewsToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                columns: new[] { "PublishDate", "Views" },
                values: new object[] { new DateTime(2025, 3, 24, 14, 52, 8, 632, DateTimeKind.Local).AddTicks(2457), 0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"),
                columns: new[] { "PublishDate", "Views" },
                values: new object[] { new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), 0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"),
                columns: new[] { "PublishDate", "Views" },
                values: new object[] { new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "Date",
                value: new DateTime(2025, 3, 24, 14, 52, 8, 633, DateTimeKind.Local).AddTicks(4043));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Games");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "Date",
                value: new DateTime(2025, 3, 22, 18, 56, 54, 392, DateTimeKind.Local).AddTicks(9706));
        }
    }
}
