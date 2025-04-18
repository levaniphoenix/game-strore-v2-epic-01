﻿// <auto-generated />
using System;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(GamestoreDBContext))]
    [Migration("20250324105209_AddPublishDateAndViewsToGame")]
    partial class AddPublishDateAndViewsToGame
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("ParentId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bce96b76-0624-4248-9b34-d30655495c2d"),
                            Body = "This is a test comment",
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            IsDeleted = false,
                            Name = "Paul"
                        },
                        new
                        {
                            Id = new Guid("525d6ae7-9e7f-4514-930f-c62428c41948"),
                            Body = "This is a test comment 2",
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            IsDeleted = false,
                            Name = "John"
                        },
                        new
                        {
                            Id = new Guid("96d2c845-1144-4a6b-a8cb-abced83a8b2c"),
                            Body = "This is a test reply to test comment",
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            IsDeleted = false,
                            Name = "Paul",
                            ParentId = new Guid("bce96b76-0624-4248-9b34-d30655495c2d")
                        },
                        new
                        {
                            Id = new Guid("66d313b8-da86-4444-aae6-98f3737624eb"),
                            Body = "This is a test reply to comment 2",
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            IsDeleted = false,
                            Name = "John",
                            ParentId = new Guid("525d6ae7-9e7f-4514-930f-c62428c41948")
                        },
                        new
                        {
                            Id = new Guid("0db6fa93-f8a1-43f5-a57c-4405bce7aa63"),
                            Body = "This is a tier 3 reply to test comment",
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            IsDeleted = false,
                            Name = "Paul",
                            ParentId = new Guid("96d2c845-1144-4a6b-a8cb-abced83a8b2c")
                        });
                });

            modelBuilder.Entity("Data.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(5000)");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UnitInStock")
                        .HasColumnType("int");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            Description = "This is a test game",
                            Discount = 1,
                            Key = "test_game",
                            Name = "Test Game",
                            Price = 10.0,
                            PublishDate = new DateTime(2025, 3, 24, 14, 52, 8, 632, DateTimeKind.Local).AddTicks(2457),
                            PublisherId = new Guid("407fb582-9b59-4dc9-89e7-49a8a6004e20"),
                            UnitInStock = 10,
                            Views = 0
                        },
                        new
                        {
                            Id = new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"),
                            Description = "This is a test game 2",
                            Discount = 2,
                            Key = "test_game_2",
                            Name = "Test Game 2",
                            Price = 20.0,
                            PublishDate = new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                            PublisherId = new Guid("4ef83756-fcfc-43d5-b3e1-b91b4a316486"),
                            UnitInStock = 20,
                            Views = 0
                        },
                        new
                        {
                            Id = new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"),
                            Description = "This is a test game 3",
                            Discount = 3,
                            Key = "test_game_3",
                            Name = "Test Game 3",
                            Price = 30.0,
                            PublishDate = new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc),
                            PublisherId = new Guid("82ee6a6b-2722-410b-ac63-a2f919fed6e4"),
                            UnitInStock = 30,
                            Views = 0
                        });
                });

            modelBuilder.Entity("Data.Entities.GameGenre", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("GameGenres");

                    b.HasData(
                        new
                        {
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            GenreId = new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e")
                        },
                        new
                        {
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            GenreId = new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7")
                        },
                        new
                        {
                            GameId = new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"),
                            GenreId = new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7")
                        },
                        new
                        {
                            GameId = new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"),
                            GenreId = new Guid("970af4f3-0145-4c47-a747-51188db37650")
                        });
                });

            modelBuilder.Entity("Data.Entities.GamePlatform", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "PlatformId");

                    b.HasIndex("PlatformId");

                    b.ToTable("GamePlatforms");

                    b.HasData(
                        new
                        {
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            PlatformId = new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5")
                        },
                        new
                        {
                            GameId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            PlatformId = new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9")
                        },
                        new
                        {
                            GameId = new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"),
                            PlatformId = new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9")
                        },
                        new
                        {
                            GameId = new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"),
                            PlatformId = new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9")
                        });
                });

            modelBuilder.Entity("Data.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<Guid?>("ParentGenreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentGenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e"),
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7"),
                            Name = "Sports Races"
                        },
                        new
                        {
                            Id = new Guid("970af4f3-0145-4c47-a747-51188db37650"),
                            Name = "Action"
                        },
                        new
                        {
                            Id = new Guid("d5bb32d9-ccdf-46b0-9859-4d9d897d39af"),
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce"),
                            Name = "Skill"
                        },
                        new
                        {
                            Id = new Guid("217165cd-1739-4a1c-a437-f768ef8bb0c7"),
                            Name = "RTS",
                            ParentGenreId = new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e")
                        },
                        new
                        {
                            Id = new Guid("387eda52-897b-4e2f-94d7-38c5620a3513"),
                            Name = "TBS",
                            ParentGenreId = new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e")
                        },
                        new
                        {
                            Id = new Guid("6344f3d4-78d1-49a9-b328-17384f8c8003"),
                            Name = "RPG",
                            ParentGenreId = new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e")
                        },
                        new
                        {
                            Id = new Guid("67435def-e5d4-45df-85db-774d03f7c88d"),
                            Name = "Rally",
                            ParentGenreId = new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7")
                        },
                        new
                        {
                            Id = new Guid("818f2abb-57ae-42d5-aab1-231a5daf5c70"),
                            Name = "Arcade",
                            ParentGenreId = new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7")
                        },
                        new
                        {
                            Id = new Guid("93a10982-1873-4e84-ae7e-54c62cf6e2d2"),
                            Name = "Formula",
                            ParentGenreId = new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7")
                        },
                        new
                        {
                            Id = new Guid("958b7723-fb54-4016-badb-86351ef95a16"),
                            Name = "Off-road",
                            ParentGenreId = new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7")
                        },
                        new
                        {
                            Id = new Guid("b0e9e6ce-1596-4dd2-a880-0257693e8b7e"),
                            Name = "FPS",
                            ParentGenreId = new Guid("970af4f3-0145-4c47-a747-51188db37650")
                        },
                        new
                        {
                            Id = new Guid("d40645c6-697a-4444-b205-206047d77b18"),
                            Name = "TPS",
                            ParentGenreId = new Guid("970af4f3-0145-4c47-a747-51188db37650")
                        },
                        new
                        {
                            Id = new Guid("d7359da5-b867-4b1a-962c-dda749590342"),
                            Name = "Puzzle",
                            ParentGenreId = new Guid("d5bb32d9-ccdf-46b0-9859-4d9d897d39af")
                        });
                });

            modelBuilder.Entity("Data.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            CustomerId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Date = new DateTime(2025, 3, 24, 14, 52, 8, 633, DateTimeKind.Local).AddTicks(4043),
                            Status = 0
                        });
                });

            modelBuilder.Entity("Data.Entities.OrderGame", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            ProductId = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"),
                            Discount = 0,
                            Price = 60.0,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("Data.Entities.Platform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("Platforms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5"),
                            Type = "Mobile"
                        },
                        new
                        {
                            Id = new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9"),
                            Type = "Browser"
                        },
                        new
                        {
                            Id = new Guid("e8ba3b5e-b3d2-4b98-a525-3516f870d6ea"),
                            Type = "Desktop"
                        },
                        new
                        {
                            Id = new Guid("f072a5e8-47d1-4778-a508-8350f53dc3a4"),
                            Type = "Console"
                        });
                });

            modelBuilder.Entity("Data.Entities.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(5000)");

                    b.Property<string>("HomePage")
                        .HasColumnType("VARCHAR(1000)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.ToTable("Publishers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("407fb582-9b59-4dc9-89e7-49a8a6004e20"),
                            CompanyName = "Test Publisher"
                        },
                        new
                        {
                            Id = new Guid("4ef83756-fcfc-43d5-b3e1-b91b4a316486"),
                            CompanyName = "Test Publisher 2"
                        },
                        new
                        {
                            Id = new Guid("82ee6a6b-2722-410b-ac63-a2f919fed6e4"),
                            CompanyName = "Test Publisher 3"
                        });
                });

            modelBuilder.Entity("Data.Entities.Comment", b =>
                {
                    b.HasOne("Data.Entities.Game", "Game")
                        .WithMany("Comments")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Comment", "Parent")
                        .WithMany("Replies")
                        .HasForeignKey("ParentId");

                    b.Navigation("Game");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Data.Entities.Game", b =>
                {
                    b.HasOne("Data.Entities.Publisher", "Publisher")
                        .WithMany("Games")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Data.Entities.GameGenre", b =>
                {
                    b.HasOne("Data.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Data.Entities.GamePlatform", b =>
                {
                    b.HasOne("Data.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("Data.Entities.Genre", b =>
                {
                    b.HasOne("Data.Entities.Genre", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentGenreId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Data.Entities.OrderGame", b =>
                {
                    b.HasOne("Data.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Game", "Product")
                        .WithMany("OrderGames")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.Entities.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("Data.Entities.Game", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("OrderGames");
                });

            modelBuilder.Entity("Data.Entities.Genre", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Data.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Data.Entities.Publisher", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
