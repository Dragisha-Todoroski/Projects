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
            return await _context.Set<VideoGame>().Include(x => x.VideoGamePlatformAvailability).ThenInclude(y => y.VideoGamePlatform).FirstOrDefaultAsync(x => x.Id == videoGameId);
        }
    }
}
