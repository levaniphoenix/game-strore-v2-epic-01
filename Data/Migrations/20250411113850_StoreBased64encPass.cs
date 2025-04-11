using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class StoreBased64encPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(2058)",
                maxLength: 2058,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(2058)",
                oldMaxLength: 2058);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "PublishDate",
                value: new DateTime(2025, 4, 11, 15, 38, 50, 360, DateTimeKind.Local).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "Date",
                value: new DateTime(2025, 4, 11, 15, 38, 50, 362, DateTimeKind.Local).AddTicks(36));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(2058)",
                maxLength: 2058,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2058)",
                oldMaxLength: 2058);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "PublishDate",
                value: new DateTime(2025, 4, 11, 14, 36, 25, 576, DateTimeKind.Local).AddTicks(8842));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "Date",
                value: new DateTime(2025, 4, 11, 14, 36, 25, 578, DateTimeKind.Local).AddTicks(955));
        }
    }
}
