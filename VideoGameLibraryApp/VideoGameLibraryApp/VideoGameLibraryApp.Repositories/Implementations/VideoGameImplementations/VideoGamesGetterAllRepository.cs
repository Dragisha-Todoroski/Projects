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
    public class VideoGamesGetterAllRepository : IVideoGamesGetterAllRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGamesGetterAllRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VideoGame>> GetAllVideoGames()
        {
            return await _context.Set<VideoGame>().Include(x => x.VideoGamePlatformAvailability).ThenInclude(y => y.VideoGamePlatform).ToListAsync();
        }
    }
}
