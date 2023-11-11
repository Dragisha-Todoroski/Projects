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
    public class VideoGamesGetterByIdService : IVideoGamesGetterByIdService
    {
        private readonly IVideoGamesGetterByIdRepository _videoGamesGetterByIdRepository;

        public VideoGamesGetterByIdService(IVideoGamesGetterByIdRepository videoGamesGetterByIdRepository)
        {
            _videoGamesGetterByIdRepository = videoGamesGetterByIdRepository;
        }

        public async Task<VideoGameResponse?> GetVideoGameById(Guid? videoGameId)
        {
            // validations
            if (videoGameId == null)
                return null;

            VideoGame? videoGame = await _videoGamesGetterByIdRepository.GetVideoGameById(videoGameId.Value);
            if (videoGame == null)
                return null;

            return videoGame.ToVideoGameResponse();
        }
    }
}
