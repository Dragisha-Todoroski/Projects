using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.DataAccess;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGamePlatformAvailabilityAbstractions;

namespace VideoGameLibraryApp.Repositories.Implementations.VideoGamePlatformAvailabilityImplementations
{
    public class VideoGamePlatformAvailabilityDeleterRepository : IVideoGamePlatformAvailabilityDeleterRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGamePlatformAvailabilityDeleterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteVideoGamePlatformAvailabilityById(Guid videoGamePlatformAvailabilityId)
        {
            _context.Set<VideoGamePlatformAvailability>().RemoveRange(_context.Set<VideoGamePlatformAvailability>().Where(x => x.Id == videoGamePlatformAvailabilityId));

            int rowsDeleted = await _context.SaveChangesAsync();

            return rowsDeleted > 0;
        }
    }
}
