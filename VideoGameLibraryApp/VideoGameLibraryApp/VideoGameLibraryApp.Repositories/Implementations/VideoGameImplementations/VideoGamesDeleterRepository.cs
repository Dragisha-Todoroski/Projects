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
    public class VideoGamesDeleterRepository : IVideoGamesDeleterRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGamesDeleterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteVideoGameById(Guid videoGameId)
        {
            _context.Set<VideoGame>().RemoveRange(_context.Set<VideoGame>().Where(x => x.Id == videoGameId));

            int rowsDeleted = await _context.SaveChangesAsync();

            return rowsDeleted > 0;
        }
    }
}
