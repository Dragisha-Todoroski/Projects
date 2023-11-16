using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;

namespace VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations
{
    public class VideoGamesGetterByTitleService : IVideoGamesGetterByTitleService
    {
        private readonly IVideoGamesGetterByTitleRepository _videoGamesGetterByTitleRepository;

        public VideoGamesGetterByTitleService(IVideoGamesGetterByTitleRepository videoGamesGetterByTitleRepository)
        {
            _videoGamesGetterByTitleRepository = videoGamesGetterByTitleRepository;
        }

        public async Task<VideoGameResponse?> GetVideoGameByTitle(string? title)
        {
            // validations
            if (title == null)
                return null;

            VideoGame? videoGame = await _videoGamesGetterByTitleRepository.GetVideoGameByTitle(title);
            if (videoGame == null) 
                return null;

            return videoGame.ToVideoGameResponse();
        }
    }
}
