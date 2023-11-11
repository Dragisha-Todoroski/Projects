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
    public class VideoGamesAdderRepository : IVideoGamesAdderRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGamesAdderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VideoGame> AddVideoGame(VideoGame videoGame)
        {
            await _context.Set<VideoGame>().AddAsync(videoGame);
            await _context.SaveChangesAsync();

            return videoGame;
        }
    }
}
