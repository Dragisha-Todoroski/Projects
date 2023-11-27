using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Domain.IdentiyEntities;

namespace VideoGameLibraryApp.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<VideoGame>? VideoGames { get; set; }
        public DbSet<VideoGamePlatform>? VideoGamePlatforms { get; set; }
        public DbSet<VideoGamePlatformAvailability>? VideoGamePlatformAvailability { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<VideoGamePlatform>()
                .HasData(
                new VideoGamePlatform()
                {
                    Id = Guid.NewGuid(),
                    Name = "PC"
                },
                new VideoGamePlatform()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nintendo Switch"
                },
                new VideoGamePlatform()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nintendo 64"
                },
                new VideoGamePlatform()
                {
                    Id = Guid.NewGuid(),
                    Name = "Xbox 360"
                },
                new VideoGamePlatform()
                {
                    Id = Guid.NewGuid(),
                    Name = "Xbox Series X/S"
                },
                new VideoGamePlatform()
                {
                    Id = Guid.NewGuid(),
                    Name = "PlayStation 4"
                },
                new VideoGamePlatform()
                {
                    Id = Guid.NewGuid(),
                    Name = "PlayStation 5"
                },
                new VideoGamePlatform()
                {
                    Id = Guid.NewGuid(),
                    Name = "Android/IOS"
                });

            var minecraftId = Guid.NewGuid();
            var itTakesTwoId = Guid.NewGuid();
            var haloId = Guid.NewGuid();

            modelBuilder.Entity<VideoGame>().HasData(
                new VideoGame
                {
                    Id = minecraftId,
                    Title = "Minecraft",
                    Genre = "Survival",
                    ReleaseDate = new DateTime(2011, 04, 17),
                    Publisher = "Mojang",
                    IsMultiplayer = true,
                    IsCoop = false
                },
                new VideoGame
                {
                    Id = itTakesTwoId,
                    Title = "It Takes Two",
                    Genre = "Platformer",
                    ReleaseDate = new DateTime(2027, 12, 03),
                    IsMultiplayer = false,
                    IsCoop = true
                },
                new VideoGame
                {
                    Id = haloId,
                    Title = "Halo",
                    Genre = "Shooter",
                    ReleaseDate = new DateTime(2001, 05, 31),
                    IsMultiplayer = true,
                    IsCoop = true
                }
            );

            // Add test data for VideoGamePlatformAvailability
            modelBuilder.Entity<VideoGamePlatformAvailability>().HasData(
                new VideoGamePlatformAvailability
                {
                    Id = Guid.NewGuid(),
                    VideoGameId = minecraftId,
                    VideoGamePlatformId = Guid.Parse("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc")
                },
                new VideoGamePlatformAvailability
                {
                    Id = Guid.NewGuid(),
                    VideoGameId = minecraftId,
                    VideoGamePlatformId = Guid.Parse("2b60a4fb-3c38-4fea-9d55-ed03151290e1")
                },
                new VideoGamePlatformAvailability
                {
                    Id= Guid.NewGuid(),
                    VideoGameId= haloId,
                    VideoGamePlatformId = Guid.Parse("ddc9382d-5e3c-453d-a35e-046e48b28f61"),
                },
                new VideoGamePlatformAvailability
                {
                    Id = Guid.NewGuid(),
                    VideoGameId = haloId,
                    VideoGamePlatformId = Guid.Parse("518a4c3e-5a6d-4ca5-b960-ce27a73d4afc"),
                },
                new VideoGamePlatformAvailability
                {
                    Id = Guid.NewGuid(),
                    VideoGameId = haloId,
                    VideoGamePlatformId = Guid.Parse("2b60a4fb-3c38-4fea-9d55-ed03151290e1"),
                },
                new VideoGamePlatformAvailability
                {
                    Id = Guid.NewGuid(),
                    VideoGameId = itTakesTwoId,
                    VideoGamePlatformId = Guid.Parse("0600e140-c1ad-42d3-a4a9-81f58bac94fd"),
                }
            );
        }
    }
}
