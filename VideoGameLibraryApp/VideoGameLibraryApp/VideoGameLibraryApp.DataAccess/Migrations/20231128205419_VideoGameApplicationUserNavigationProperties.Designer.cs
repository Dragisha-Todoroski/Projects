﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoGameLibraryApp.DataAccess;

#nullable disable

namespace VideoGameLibraryApp.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231128205419_VideoGameApplicationUserNavigationProperties")]
    partial class VideoGameApplicationUserNavigationProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.Entities.VideoGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsCoop")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMultiplayer")
                        .HasColumnType("bit");

                    b.Property<string>("Publisher")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("VideoGames");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00c75070-3f00-4d52-ac66-de5ed23d49cf"),
                            Genre = "Survival",
                            IsCoop = false,
                            IsMultiplayer = true,
                            Publisher = "Mojang",
                            ReleaseDate = new DateTime(2011, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Minecraft",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("697ca897-1a64-4e61-ad5e-c18429d191e2"),
                            Genre = "Platformer",
                            IsCoop = true,
                            IsMultiplayer = false,
                            ReleaseDate = new DateTime(2027, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "It Takes Two",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = new Guid("e0075acc-35ae-47a1-923c-6622a1681f3a"),
                            Genre = "Shooter",
                            IsCoop = true,
                            IsMultiplayer = true,
                            ReleaseDate = new DateTime(2001, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Halo",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.Entities.VideoGamePlatform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("VideoGamePlatforms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bafd6f7c-8ede-4b13-ab0b-33c9714e5f3c"),
                            Name = "PC"
                        },
                        new
                        {
                            Id = new Guid("988cd850-234e-49a9-aa12-b26d9ee0d3cc"),
                            Name = "Nintendo Switch"
                        },
                        new
                        {
                            Id = new Guid("f447345a-f479-4535-9c6b-d35c5ac0d661"),
                            Name = "Nintendo 64"
                        },
                        new
                        {
                            Id = new Guid("ad59cf02-1f0b-462c-a569-e089d4317b3a"),
                            Name = "Xbox 360"
                        },
                        new
                        {
                            Id = new Guid("8a57efa9-86b0-47be-aee5-41ede8296a1b"),
                            Name = "Xbox Series X/S"
                        },
                        new
                        {
                            Id = new Guid("905585dc-28ab-48a6-81ff-1c111afa5e17"),
                            Name = "PlayStation 4"
                        },
                        new
                        {
                            Id = new Guid("d8354a93-d5fd-4850-8bb1-472593826b4e"),
                            Name = "PlayStation 5"
                        },
                        new
                        {
                            Id = new Guid("e8d12fde-eb35-4d60-a2de-11a93f717533"),
                            Name = "Android/IOS"
                        });
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.Entities.VideoGamePlatformAvailability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VideoGameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VideoGamePlatformId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VideoGameId");

                    b.HasIndex("VideoGamePlatformId");

                    b.ToTable("VideoGamePlatformAvailability");

                    b.HasData(
                        new
                        {
                            Id = new Guid("18b28ed7-5417-4694-98f9-74a0e72eb176"),
                            VideoGameId = new Guid("00c75070-3f00-4d52-ac66-de5ed23d49cf"),
                            VideoGamePlatformId = new Guid("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc")
                        },
                        new
                        {
                            Id = new Guid("15553226-aba8-4b75-bf70-0bfcf1b0831d"),
                            VideoGameId = new Guid("00c75070-3f00-4d52-ac66-de5ed23d49cf"),
                            VideoGamePlatformId = new Guid("2b60a4fb-3c38-4fea-9d55-ed03151290e1")
                        },
                        new
                        {
                            Id = new Guid("e7641796-064a-4525-94c4-fe3017b8362f"),
                            VideoGameId = new Guid("e0075acc-35ae-47a1-923c-6622a1681f3a"),
                            VideoGamePlatformId = new Guid("ddc9382d-5e3c-453d-a35e-046e48b28f61")
                        },
                        new
                        {
                            Id = new Guid("6a6d72b2-b2de-44d1-b2a5-ceb52e65be50"),
                            VideoGameId = new Guid("e0075acc-35ae-47a1-923c-6622a1681f3a"),
                            VideoGamePlatformId = new Guid("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc")
                        },
                        new
                        {
                            Id = new Guid("7922b692-8ca4-4f35-8fab-3ecb6c81b8c9"),
                            VideoGameId = new Guid("e0075acc-35ae-47a1-923c-6622a1681f3a"),
                            VideoGamePlatformId = new Guid("2b60a4fb-3c38-4fea-9d55-ed03151290e1")
                        },
                        new
                        {
                            Id = new Guid("464639ea-fda0-486e-91fc-453e2d944667"),
                            VideoGameId = new Guid("697ca897-1a64-4e61-ad5e-c18429d191e2"),
                            VideoGamePlatformId = new Guid("0600e140-c1ad-42d3-a4a9-81f58bac94fd")
                        });
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.Entities.VideoGame", b =>
                {
                    b.HasOne("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationUser", "User")
                        .WithMany("VideoGames")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.Entities.VideoGamePlatformAvailability", b =>
                {
                    b.HasOne("VideoGameLibraryApp.Domain.Entities.VideoGame", "VideoGame")
                        .WithMany("VideoGamePlatformAvailability")
                        .HasForeignKey("VideoGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VideoGameLibraryApp.Domain.Entities.VideoGamePlatform", "VideoGamePlatform")
                        .WithMany("VideoGamePlatformAvailability")
                        .HasForeignKey("VideoGamePlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VideoGame");

                    b.Navigation("VideoGamePlatform");
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.Entities.VideoGame", b =>
                {
                    b.Navigation("VideoGamePlatformAvailability");
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.Entities.VideoGamePlatform", b =>
                {
                    b.Navigation("VideoGamePlatformAvailability");
                });

            modelBuilder.Entity("VideoGameLibraryApp.Domain.IdentiyEntities.ApplicationUser", b =>
                {
                    b.Navigation("VideoGames");
                });
#pragma warning restore 612, 618
        }
    }
}