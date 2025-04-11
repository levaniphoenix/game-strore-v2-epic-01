using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206") });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "PublishDate",
                value: new DateTime(2025, 4, 11, 23, 5, 42, 807, DateTimeKind.Local).AddTicks(36));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "Date", "Status" },
                values: new object[] { new Guid("89866a29-ebf5-46b0-9e82-91ef26ec0447"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 11, 23, 5, 42, 808, DateTimeKind.Local).AddTicks(1455), 0 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2d32286b-8ae7-4579-92e8-0a8afa70d6ec"), "Has read-only access.", "Guest" },
                    { new Guid("64614001-fec4-4889-928e-61b4aab78665"), "Can manage users and roles. Can see deleted games. Can manage comments for the deleted game. Can edit a deleted game.", "Admin" },
                    { new Guid("7a24a152-00a7-42fb-ad05-ebb5b71ef264"), "Can manage game comments. Can ban users from commenting.", "Moderator" },
                    { new Guid("8e316382-2646-4484-a79b-3a78a41131d2"), "Can manage business entities: games, genres, publishers, platforms, etc. Can edit orders. Can view orders history. Can’t edit orders from history. Can change the status of an order from paid to shipped. Can't edit a deleted game.", "Manager" },
                    { new Guid("c54327ae-8a79-4dd0-9670-f70786ebd7f6"), "Can’t see deleted games. Can’t buy a deleted game. Can see the games in stock. Can comment game.", "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BannedUntil", "Email", "FirstName", "IsBanned", "IsExternalUser", "LastName", "PasswordHash" },
                values: new object[] { new Guid("fc329ddf-39cd-4478-abe5-85663ca2659d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", false, false, "admin", "plPkIXrCzVHytBtFEPhraFJrrik7Z5j1XfNlIB6532A=-JxvDJ4ckFVbKUHJKX5HR+g==" });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "Discount", "Price", "Quantity" },
                values: new object[] { new Guid("89866a29-ebf5-46b0-9e82-91ef26ec0447"), new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), 0, 60.0, 1 });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("64614001-fec4-4889-928e-61b4aab78665"), new Guid("fc329ddf-39cd-4478-abe5-85663ca2659d") },
                    { new Guid("8e316382-2646-4484-a79b-3a78a41131d2"), new Guid("fc329ddf-39cd-4478-abe5-85663ca2659d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("89866a29-ebf5-46b0-9e82-91ef26ec0447"), new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2d32286b-8ae7-4579-92e8-0a8afa70d6ec"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7a24a152-00a7-42fb-ad05-ebb5b71ef264"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c54327ae-8a79-4dd0-9670-f70786ebd7f6"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("64614001-fec4-4889-928e-61b4aab78665"), new Guid("fc329ddf-39cd-4478-abe5-85663ca2659d") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8e316382-2646-4484-a79b-3a78a41131d2"), new Guid("fc329ddf-39cd-4478-abe5-85663ca2659d") });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("89866a29-ebf5-46b0-9e82-91ef26ec0447"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("64614001-fec4-4889-928e-61b4aab78665"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8e316382-2646-4484-a79b-3a78a41131d2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fc329ddf-39cd-4478-abe5-85663ca2659d"));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                column: "PublishDate",
                value: new DateTime(2025, 4, 11, 15, 38, 50, 360, DateTimeKind.Local).AddTicks(8677));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "Date", "Status" },
                values: new object[] { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 11, 15, 38, 50, 362, DateTimeKind.Local).AddTicks(36), 0 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId", "Discount", "Price", "Quantity" },
                values: new object[] { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), 0, 60.0, 1 });
        }
    }
}
