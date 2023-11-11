using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.DataAccess;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGamePlatformAbstractions;

namespace VideoGameLibraryApp.Repositories.Implementations.VideoGamePlatformImplementations
{
    public class VideoGamePlatformsGetterAllRepository : IVideoGamePlatformsGetterAllRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoGamePlatformsGetterAllRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VideoGamePlatform>> GetAllVideoGamePlatforms()
        {
            return await _context.Set<VideoGamePlatform>().Include(x => x.VideoGamePlatformAvailability).ThenInclude(y => y.VideoGame).ToListAsync();
        }
    }
}
