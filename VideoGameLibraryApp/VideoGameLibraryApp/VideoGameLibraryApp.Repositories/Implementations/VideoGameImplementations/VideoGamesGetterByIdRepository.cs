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
    public class VideoGamesGetterByIdRepository : IVideoGamesGetterByIdRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGamesGetterByIdRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VideoGame?> GetVideoGameById(Guid videoGameId)
        {
            var videoGame = await _context.Set<VideoGame>()
                .Include(x => x.VideoGamePlatformAvailability)
                .FirstOrDefaultAsync(y => y.Id == videoGameId);

            // If VideoGamePlatformAvailability has at least one item in it, load the related VideoGamePlatform
            if (videoGame != null && videoGame.VideoGamePlatformAvailability != null)
            {
                _context.Entry(videoGame)
                    .Collection(x => x.VideoGamePlatformAvailability!)
                    .Query()
                    .Include(y => y.VideoGamePlatform)
                    .Load();
            }

            return videoGame;
        }
    }
}
