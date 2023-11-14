using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Exceptions.CustomExceptions;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;
using VideoGameLibraryApp.Services.ServiceHelpers;

namespace VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations
{
    public class VideoGamesAdderService : IVideoGamesAdderService
    {
        private readonly IVideoGamesAdderRepository _videoGamesAdderRepository;

        public VideoGamesAdderService(IVideoGamesAdderRepository videoGamesAdderRepository)
        {
            _videoGamesAdderRepository = videoGamesAdderRepository;
        }

        public async Task<VideoGameResponse> AddVideoGame(VideoGameAddRequest? videoGameAddRequest)
        {
            // validations
            if (videoGameAddRequest == null)
            {
                throw new ArgumentNullException(nameof(videoGameAddRequest));
            }

            ValidationHelper.ModelValidation(videoGameAddRequest);

            VideoGame videoGame = videoGameAddRequest.ToVideoGame();

            videoGame.Id = Guid.NewGuid();

            var platforms = videoGameAddRequest.VideoGamePlatformIds;

            if (platforms != null)
            {
                foreach (var platformId in platforms)
                {
                    var videoGamePlatformAvailability = new VideoGamePlatformAvailability()
                    {
                        VideoGameId = videoGame.Id,
                        VideoGamePlatformId = platformId
                    };

                    videoGame.VideoGamePlatformAvailability?.Add(videoGamePlatformAvailability);
                }
            }

            await _videoGamesAdderRepository.AddVideoGame(videoGame);

            return videoGame.ToVideoGameResponse();
        }
    }
}
