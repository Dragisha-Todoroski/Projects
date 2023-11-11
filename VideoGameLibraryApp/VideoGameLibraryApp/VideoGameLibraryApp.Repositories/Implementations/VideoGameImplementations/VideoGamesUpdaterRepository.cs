using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.DataAccess;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;

namespace VideoGameLibraryApp.Repositories.Implementations.VideoGameImplementations
{
    public class VideoGamesUpdaterRepository : IVideoGamesUpdaterRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGamesUpdaterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VideoGame> UpdateVideoGame(VideoGame videoGame)
        {
            VideoGame? dbVideoGame = await _context.Set<VideoGame>().Include(nameof(VideoGamePlatformAvailability)).FirstOrDefaultAsync(x => x.Id == videoGame.Id);
            if (dbVideoGame == null)
            {
                return videoGame;
            }

            dbVideoGame.Title = videoGame.Title;
            dbVideoGame.Genre = videoGame.Genre;
            dbVideoGame.ReleaseDate = videoGame.ReleaseDate;
            dbVideoGame.VideoGamePlatformAvailability = videoGame.VideoGamePlatformAvailability;
            dbVideoGame.Publisher = videoGame.Publisher;
            dbVideoGame.IsMultiplayer = videoGame.IsMultiplayer;
            dbVideoGame.IsCoop = videoGame.IsCoop;

            await _context.SaveChangesAsync();

            return dbVideoGame;
        }
    }
}
