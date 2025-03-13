using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGamesWithPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") });

            migrationBuilder.DeleteData(
                table: "GamePlatforms",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5") });

            migrationBuilder.DeleteData(
                table: "GamePlatforms",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("e8ba3b5e-b3d2-4b98-a525-3516f870d6ea") });

            migrationBuilder.DeleteData(
                table: "GamePlatforms",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5") });

            migrationBuilder.DeleteData(
                table: "GamePlatforms",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5") });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("217165cd-1739-4a1c-a437-f768ef8bb0c7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("387eda52-897b-4e2f-94d7-38c5620a3513"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("6344f3d4-78d1-49a9-b328-17384f8c8003"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("67435def-e5d4-45df-85db-774d03f7c88d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("818f2abb-57ae-42d5-aab1-231a5daf5c70"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("93a10982-1873-4e84-ae7e-54c62cf6e2d2"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("958b7723-fb54-4016-badb-86351ef95a16"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("970af4f3-0145-4c47-a747-51188db37650"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b0e9e6ce-1596-4dd2-a880-0257693e8b7e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d40645c6-697a-4444-b205-206047d77b18"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d7359da5-b867-4b1a-962c-dda749590342"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("f072a5e8-47d1-4778-a508-8350f53dc3a4"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d5bb32d9-ccdf-46b0-9859-4d9d897d39af"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("e8ba3b5e-b3d2-4b98-a525-3516f870d6ea"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("407fb582-9b59-4dc9-89e7-49a8a6004e20"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("4ef83756-fcfc-43d5-b3e1-b91b4a316486"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("82ee6a6b-2722-410b-ac63-a2f919fed6e4"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955"), "Sports Races", null },
                    { new Guid("2b35d042-e27f-4972-a1df-83717c659e01"), "Strategy", null },
                    { new Guid("7d336749-1b21-4544-a4a4-4fff227ea169"), "Action", null },
                    { new Guid("a46c9bca-3927-4489-9617-c6a7f8caa08a"), "Skill", null },
                    { new Guid("f650506e-26a6-4edc-b916-57d86c3699ca"), "Adventure", null }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("469124cf-fd5c-47c5-b663-a7de3d72548a"), "Console" },
                    { new Guid("70e101a4-5fda-4a81-9e19-d1a1bc88626c"), "Mobile" },
                    { new Guid("7a908eec-134e-462f-bb46-19b6c1388aeb"), "Desktop" },
                    { new Guid("a5848e58-047a-4048-9d5f-94d182effcd5"), "Browser" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "Description", "HomePage" },
                values: new object[,]
                {
                    { new Guid("a992fdf7-8eca-4935-931d-1ccb8b575203"), "Test Publisher 2", null, null },
                    { new Guid("cb37baff-566f-4295-866f-4063d4576641"), "Test Publisher", null, null },
                    { new Guid("da64e882-561a-4f55-94ad-a7a132511f03"), "Test Publisher 3", null, null }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Discount", "Key", "Name", "Price", "PublisherId", "UnitsInStock" },
                values: new object[,]
                {
                    { new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"), "This is a test game 3", 3, "test_game_3", "Test Game 3", 30.0, new Guid("da64e882-561a-4f55-94ad-a7a132511f03"), 30 },
                    { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), "This is a test game", 1, "test_game", "Test Game", 10.0, new Guid("cb37baff-566f-4295-866f-4063d4576641"), 10 },
                    { new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"), "This is a test game 2", 2, "test_game_2", "Test Game 2", 20.0, new Guid("a992fdf7-8eca-4935-931d-1ccb8b575203"), 20 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("073ded22-2ec8-47a7-b5be-e9009278acd8"), "RPG", new Guid("2b35d042-e27f-4972-a1df-83717c659e01") },
                    { new Guid("09ee67e8-0b1e-4032-90ca-ebea09d795d4"), "Rally", new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955") },
                    { new Guid("277d76b3-011f-4766-b8f3-b0b7dd233547"), "Off-road", new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955") },
                    { new Guid("844990d0-4c63-4341-8e07-e6971eb30ba0"), "TBS", new Guid("2b35d042-e27f-4972-a1df-83717c659e01") },
                    { new Guid("912e0095-d03f-4536-ae6d-ceb619d5f4b7"), "TPS", new Guid("7d336749-1b21-4544-a4a4-4fff227ea169") },
                    { new Guid("99af887d-a2f0-44f4-8875-50dc38431027"), "RTS", new Guid("2b35d042-e27f-4972-a1df-83717c659e01") },
                    { new Guid("b27aca41-1259-474a-ac67-88386ea22878"), "Puzzle", new Guid("f650506e-26a6-4edc-b916-57d86c3699ca") },
                    { new Guid("c0d82a46-43e2-4c6d-b60f-3d611d24a431"), "FPS", new Guid("7d336749-1b21-4544-a4a4-4fff227ea169") },
                    { new Guid("dbeb1a04-65fd-4a7f-bf7c-c9ae068710ad"), "Formula", new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955") },
                    { new Guid("e75ac2df-d353-4521-aca1-5ddcdc591600"), "Arcade", new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955") }
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "GameId", "GenreId" },
                values: new object[,]
                {
                    { new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"), new Guid("7d336749-1b21-4544-a4a4-4fff227ea169") },
                    { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955") },
                    { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), new Guid("2b35d042-e27f-4972-a1df-83717c659e01") },
                    { new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"), new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955") }
                });

            migrationBuilder.InsertData(
                table: "GamePlatforms",
                columns: new[] { "GameId", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"), new Guid("a5848e58-047a-4048-9d5f-94d182effcd5") },
                    { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), new Guid("70e101a4-5fda-4a81-9e19-d1a1bc88626c") },
                    { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), new Guid("a5848e58-047a-4048-9d5f-94d182effcd5") },
                    { new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"), new Guid("a5848e58-047a-4048-9d5f-94d182effcd5") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"), new Guid("7d336749-1b21-4544-a4a4-4fff227ea169") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), new Guid("2b35d042-e27f-4972-a1df-83717c659e01") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"), new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955") });

            migrationBuilder.DeleteData(
                table: "GamePlatforms",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"), new Guid("a5848e58-047a-4048-9d5f-94d182effcd5") });

            migrationBuilder.DeleteData(
                table: "GamePlatforms",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), new Guid("70e101a4-5fda-4a81-9e19-d1a1bc88626c") });

            migrationBuilder.DeleteData(
                table: "GamePlatforms",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"), new Guid("a5848e58-047a-4048-9d5f-94d182effcd5") });

            migrationBuilder.DeleteData(
                table: "GamePlatforms",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"), new Guid("a5848e58-047a-4048-9d5f-94d182effcd5") });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("073ded22-2ec8-47a7-b5be-e9009278acd8"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("09ee67e8-0b1e-4032-90ca-ebea09d795d4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("277d76b3-011f-4766-b8f3-b0b7dd233547"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("844990d0-4c63-4341-8e07-e6971eb30ba0"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("912e0095-d03f-4536-ae6d-ceb619d5f4b7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("99af887d-a2f0-44f4-8875-50dc38431027"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a46c9bca-3927-4489-9617-c6a7f8caa08a"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b27aca41-1259-474a-ac67-88386ea22878"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("c0d82a46-43e2-4c6d-b60f-3d611d24a431"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("dbeb1a04-65fd-4a7f-bf7c-c9ae068710ad"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("e75ac2df-d353-4521-aca1-5ddcdc591600"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("469124cf-fd5c-47c5-b663-a7de3d72548a"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("7a908eec-134e-462f-bb46-19b6c1388aeb"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2b35d042-e27f-4972-a1df-83717c659e01"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7d336749-1b21-4544-a4a4-4fff227ea169"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f650506e-26a6-4edc-b916-57d86c3699ca"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("70e101a4-5fda-4a81-9e19-d1a1bc88626c"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("a5848e58-047a-4048-9d5f-94d182effcd5"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a992fdf7-8eca-4935-931d-1ccb8b575203"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("cb37baff-566f-4295-866f-4063d4576641"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("da64e882-561a-4f55-94ad-a7a132511f03"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e"), "Strategy", null },
                    { new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7"), "Action", null },
                    { new Guid("970af4f3-0145-4c47-a747-51188db37650"), "Skill", null },
                    { new Guid("d5bb32d9-ccdf-46b0-9859-4d9d897d39af"), "Adventure", null },
                    { new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce"), "Sports Races", null }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5"), "Browser" },
                    { new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9"), "Console" },
                    { new Guid("e8ba3b5e-b3d2-4b98-a525-3516f870d6ea"), "Mobile" },
                    { new Guid("f072a5e8-47d1-4778-a508-8350f53dc3a4"), "Desktop" }
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
                columns: new[] { "Id", "Description", "Discount", "Key", "Name", "Price", "PublisherId", "UnitsInStock" },
                values: new object[,]
                {
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), "This is a test game", 0, "test_game", "Test Game", 0.0, new Guid("407fb582-9b59-4dc9-89e7-49a8a6004e20"), 0 },
                    { new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), "This is a test game 3", 0, "test_game_3", "Test Game 3", 0.0, new Guid("82ee6a6b-2722-410b-ac63-a2f919fed6e4"), 0 },
                    { new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), "This is a test game 2", 0, "test_game_2", "Test Game 2", 0.0, new Guid("4ef83756-fcfc-43d5-b3e1-b91b4a316486"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("217165cd-1739-4a1c-a437-f768ef8bb0c7"), "Off-road", new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") },
                    { new Guid("387eda52-897b-4e2f-94d7-38c5620a3513"), "TPS", new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
                    { new Guid("6344f3d4-78d1-49a9-b328-17384f8c8003"), "Arcade", new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") },
                    { new Guid("67435def-e5d4-45df-85db-774d03f7c88d"), "Puzzle", new Guid("d5bb32d9-ccdf-46b0-9859-4d9d897d39af") },
                    { new Guid("818f2abb-57ae-42d5-aab1-231a5daf5c70"), "Rally", new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") },
                    { new Guid("93a10982-1873-4e84-ae7e-54c62cf6e2d2"), "RPG", new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") },
                    { new Guid("958b7723-fb54-4016-badb-86351ef95a16"), "RTS", new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") },
                    { new Guid("b0e9e6ce-1596-4dd2-a880-0257693e8b7e"), "TBS", new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") },
                    { new Guid("d40645c6-697a-4444-b205-206047d77b18"), "Formula", new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") },
                    { new Guid("d7359da5-b867-4b1a-962c-dda749590342"), "FPS", new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") }
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "GameId", "GenreId" },
                values: new object[,]
                {
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e") },
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") },
                    { new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
                    { new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") }
                });

            migrationBuilder.InsertData(
                table: "GamePlatforms",
                columns: new[] { "GameId", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5") },
                    { new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), new Guid("e8ba3b5e-b3d2-4b98-a525-3516f870d6ea") },
                    { new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5") },
                    { new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5") }
                });
        }
    }
}
