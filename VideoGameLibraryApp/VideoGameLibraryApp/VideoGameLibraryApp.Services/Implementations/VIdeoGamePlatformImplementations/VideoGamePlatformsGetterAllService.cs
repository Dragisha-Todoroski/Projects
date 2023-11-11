using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGamePlatformAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGamePlatformAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGamePlatformDTOs;

namespace VideoGameLibraryApp.Services.Implementations.VIdeoGamePlatformImplementations
{
    public class VideoGamePlatformsGetterAllService : IVideoGamePlatformsGetterAllService
    {
        private readonly IVideoGamePlatformsGetterAllRepository _videoGamePlatformsGetterAllRepository;

        public VideoGamePlatformsGetterAllService(IVideoGamePlatformsGetterAllRepository videoGamePlatformsGetterAllRepository)
        {
            _videoGamePlatformsGetterAllRepository = videoGamePlatformsGetterAllRepository;
        }

        public async Task<List<VideoGamePlatformResponse>> GetAllVideoGamePlatforms()
        {
            List<VideoGamePlatform> videoGamePlatforms = await _videoGamePlatformsGetterAllRepository.GetAllVideoGamePlatforms();

            return videoGamePlatforms.Select(x => x.ToVideoGamePlatformResponse()).ToList();
        }
    }
}
