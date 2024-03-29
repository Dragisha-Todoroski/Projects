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
    [Migration("20231127195217_RemovedDisplayNameInApplicationUser")]
    partial class RemovedDisplayNameInApplicationUser
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

                    b.HasKey("Id");

                    b.ToTable("VideoGames");

                    b.HasData(
                        new
                        {
                            Id = new Guid("03b5a674-042f-4b43-a23f-44e2264f0ccc"),
                            Genre = "Survival",
                            IsCoop = false,
                            IsMultiplayer = true,
                            Publisher = "Mojang",
                            ReleaseDate = new DateTime(2011, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Minecraft"
                        },
                        new
                        {
                            Id = new Guid("b436970e-b327-4f68-b450-a8b507398a90"),
                            Genre = "Platformer",
                            IsCoop = true,
                            IsMultiplayer = false,
                            ReleaseDate = new DateTime(2027, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "It Takes Two"
                        },
                        new
                        {
                            Id = new Guid("55a05148-2325-4adf-8bcc-68c2ddb0d831"),
                            Genre = "Shooter",
                            IsCoop = true,
                            IsMultiplayer = true,
                            ReleaseDate = new DateTime(2001, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Halo"
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
                            Id = new Guid("113d10f3-f964-4696-9703-fe36c0d48241"),
                            Name = "PC"
                        },
                        new
                        {
                            Id = new Guid("e8aec99d-b01b-45dc-a477-60a9500ee167"),
                            Name = "Nintendo Switch"
                        },
                        new
                        {
                            Id = new Guid("78a8add8-df90-4dc1-af69-63d3fe855091"),
                            Name = "Nintendo 64"
                        },
                        new
                        {
                            Id = new Guid("c5cd4e82-d99e-4e84-b78c-c36711d58558"),
                            Name = "Xbox 360"
                        },
                        new
                        {
                            Id = new Guid("f6408ef3-ff72-4825-927b-7c04041e2bb8"),
                            Name = "Xbox Series X/S"
                        },
                        new
                        {
                            Id = new Guid("dfbfee55-bbc8-45c7-b284-a3bff7c3c5f1"),
                            Name = "PlayStation 4"
                        },
                        new
                        {
                            Id = new Guid("96eeb442-090d-4ddd-bf6f-d04c199e5162"),
                            Name = "PlayStation 5"
                        },
                        new
                        {
                            Id = new Guid("f6953e04-920d-4d31-b821-ca5705b040fb"),
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
                            Id = new Guid("c62446f9-d8ff-499b-baf6-9a75b5e37bfa"),
                            VideoGameId = new Guid("03b5a674-042f-4b43-a23f-44e2264f0ccc"),
                            VideoGamePlatformId = new Guid("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc")
                        },
                        new
                        {
                            Id = new Guid("da5ab1e3-8f08-4727-a37f-5e11cc73c619"),
                            VideoGameId = new Guid("03b5a674-042f-4b43-a23f-44e2264f0ccc"),
                            VideoGamePlatformId = new Guid("2b60a4fb-3c38-4fea-9d55-ed03151290e1")
                        },
                        new
                        {
                            Id = new Guid("ecba4568-deb7-4473-85c2-305bc23a7c04"),
                            VideoGameId = new Guid("55a05148-2325-4adf-8bcc-68c2ddb0d831"),
                            VideoGamePlatformId = new Guid("ddc9382d-5e3c-453d-a35e-046e48b28f61")
                        },
                        new
                        {
                            Id = new Guid("e7111421-3f13-4efd-9c7d-ee581ebe7de9"),
                            VideoGameId = new Guid("55a05148-2325-4adf-8bcc-68c2ddb0d831"),
                            VideoGamePlatformId = new Guid("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc")
                        },
                        new
                        {
                            Id = new Guid("847539ea-d15a-4843-8b92-69816a1e76d3"),
                            VideoGameId = new Guid("55a05148-2325-4adf-8bcc-68c2ddb0d831"),
                            VideoGamePlatformId = new Guid("2b60a4fb-3c38-4fea-9d55-ed03151290e1")
                        },
                        new
                        {
                            Id = new Guid("e72247db-4477-4df1-9a4b-dd043c6a802f"),
                            VideoGameId = new Guid("b436970e-b327-4f68-b450-a8b507398a90"),
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
#pragma warning restore 612, 618
        }
    }
}
