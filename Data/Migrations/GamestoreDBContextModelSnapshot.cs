﻿// <auto-generated />
using System;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(GamestoreDBContext))]
    partial class GamestoreDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UnitsInStock")
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
                            Id = new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"),
                            Description = "This is a test game",
                            Discount = 1,
                            Key = "test_game",
                            Name = "Test Game",
                            Price = 10.0,
                            PublisherId = new Guid("cb37baff-566f-4295-866f-4063d4576641"),
                            UnitsInStock = 10
                        },
                        new
                        {
                            Id = new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"),
                            Description = "This is a test game 2",
                            Discount = 2,
                            Key = "test_game_2",
                            Name = "Test Game 2",
                            Price = 20.0,
                            PublisherId = new Guid("a992fdf7-8eca-4935-931d-1ccb8b575203"),
                            UnitsInStock = 20
                        },
                        new
                        {
                            Id = new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"),
                            Description = "This is a test game 3",
                            Discount = 3,
                            Key = "test_game_3",
                            Name = "Test Game 3",
                            Price = 30.0,
                            PublisherId = new Guid("da64e882-561a-4f55-94ad-a7a132511f03"),
                            UnitsInStock = 30
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
                            GameId = new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"),
                            GenreId = new Guid("2b35d042-e27f-4972-a1df-83717c659e01")
                        },
                        new
                        {
                            GameId = new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"),
                            GenreId = new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955")
                        },
                        new
                        {
                            GameId = new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"),
                            GenreId = new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955")
                        },
                        new
                        {
                            GameId = new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"),
                            GenreId = new Guid("7d336749-1b21-4544-a4a4-4fff227ea169")
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
                            GameId = new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"),
                            PlatformId = new Guid("70e101a4-5fda-4a81-9e19-d1a1bc88626c")
                        },
                        new
                        {
                            GameId = new Guid("32f7c489-c6b1-42f4-a700-13d2009da8d7"),
                            PlatformId = new Guid("a5848e58-047a-4048-9d5f-94d182effcd5")
                        },
                        new
                        {
                            GameId = new Guid("4d59c252-72fc-41c7-94b5-270c0871f1f0"),
                            PlatformId = new Guid("a5848e58-047a-4048-9d5f-94d182effcd5")
                        },
                        new
                        {
                            GameId = new Guid("2d81219d-a99d-4afa-bb82-bdcecc8e8c56"),
                            PlatformId = new Guid("a5848e58-047a-4048-9d5f-94d182effcd5")
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
                            Id = new Guid("2b35d042-e27f-4972-a1df-83717c659e01"),
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955"),
                            Name = "Sports Races"
                        },
                        new
                        {
                            Id = new Guid("7d336749-1b21-4544-a4a4-4fff227ea169"),
                            Name = "Action"
                        },
                        new
                        {
                            Id = new Guid("f650506e-26a6-4edc-b916-57d86c3699ca"),
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = new Guid("a46c9bca-3927-4489-9617-c6a7f8caa08a"),
                            Name = "Skill"
                        },
                        new
                        {
                            Id = new Guid("99af887d-a2f0-44f4-8875-50dc38431027"),
                            Name = "RTS",
                            ParentGenreId = new Guid("2b35d042-e27f-4972-a1df-83717c659e01")
                        },
                        new
                        {
                            Id = new Guid("844990d0-4c63-4341-8e07-e6971eb30ba0"),
                            Name = "TBS",
                            ParentGenreId = new Guid("2b35d042-e27f-4972-a1df-83717c659e01")
                        },
                        new
                        {
                            Id = new Guid("073ded22-2ec8-47a7-b5be-e9009278acd8"),
                            Name = "RPG",
                            ParentGenreId = new Guid("2b35d042-e27f-4972-a1df-83717c659e01")
                        },
                        new
                        {
                            Id = new Guid("09ee67e8-0b1e-4032-90ca-ebea09d795d4"),
                            Name = "Rally",
                            ParentGenreId = new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955")
                        },
                        new
                        {
                            Id = new Guid("e75ac2df-d353-4521-aca1-5ddcdc591600"),
                            Name = "Arcade",
                            ParentGenreId = new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955")
                        },
                        new
                        {
                            Id = new Guid("dbeb1a04-65fd-4a7f-bf7c-c9ae068710ad"),
                            Name = "Formula",
                            ParentGenreId = new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955")
                        },
                        new
                        {
                            Id = new Guid("277d76b3-011f-4766-b8f3-b0b7dd233547"),
                            Name = "Off-road",
                            ParentGenreId = new Guid("005d8299-ff9d-47b7-b960-0afd23c3e955")
                        },
                        new
                        {
                            Id = new Guid("c0d82a46-43e2-4c6d-b60f-3d611d24a431"),
                            Name = "FPS",
                            ParentGenreId = new Guid("7d336749-1b21-4544-a4a4-4fff227ea169")
                        },
                        new
                        {
                            Id = new Guid("912e0095-d03f-4536-ae6d-ceb619d5f4b7"),
                            Name = "TPS",
                            ParentGenreId = new Guid("7d336749-1b21-4544-a4a4-4fff227ea169")
                        },
                        new
                        {
                            Id = new Guid("b27aca41-1259-474a-ac67-88386ea22878"),
                            Name = "Puzzle",
                            ParentGenreId = new Guid("f650506e-26a6-4edc-b916-57d86c3699ca")
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
                            Id = new Guid("70e101a4-5fda-4a81-9e19-d1a1bc88626c"),
                            Type = "Mobile"
                        },
                        new
                        {
                            Id = new Guid("a5848e58-047a-4048-9d5f-94d182effcd5"),
                            Type = "Browser"
                        },
                        new
                        {
                            Id = new Guid("7a908eec-134e-462f-bb46-19b6c1388aeb"),
                            Type = "Desktop"
                        },
                        new
                        {
                            Id = new Guid("469124cf-fd5c-47c5-b663-a7de3d72548a"),
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
                            Id = new Guid("cb37baff-566f-4295-866f-4063d4576641"),
                            CompanyName = "Test Publisher"
                        },
                        new
                        {
                            Id = new Guid("a992fdf7-8eca-4935-931d-1ccb8b575203"),
                            CompanyName = "Test Publisher 2"
                        },
                        new
                        {
                            Id = new Guid("da64e882-561a-4f55-94ad-a7a132511f03"),
                            CompanyName = "Test Publisher 3"
                        });
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

            modelBuilder.Entity("Data.Entities.Genre", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Data.Entities.Publisher", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
