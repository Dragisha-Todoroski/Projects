using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Services.DTOs.VideoGamePlatformDTOs;

namespace VideoGameLibraryApp.Services.Abstractions.VideoGamePlatformAbstractions
{
    public interface IVideoGamePlatformsGetterAllService
    {
        Task<List<VideoGamePlatformResponse>> GetAllVideoGamePlatforms();
    }
}
