using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.DataAccess;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;

namespace VideoGameLibraryApp.Repositories.Implementations.VideoGameImplementations
{
    public class VideoGamesGetterByTitleRepository : IVideoGamesGetterByTitleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VideoGamesGetterByTitleRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<VideoGame?> GetVideoGameByTitle(string title)
        {
            var videoGame = await _context.Set<VideoGame>()
                .Include(x => x.User)
                .Include(y => y.VideoGamePlatformAvailability)
                .FirstOrDefaultAsync(z => z.Title == title && z.UserId == Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

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
