using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Exceptions.CustomExceptions;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGamePlatformAvailabilityAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;
using VideoGameLibraryApp.Services.ServiceHelpers;

namespace VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations
{
    public class VideoGamesUpdaterService : IVideoGamesUpdaterService
    {
        private readonly IVideoGamesUpdaterRepository _videoGamesUpdaterRepository;
        private readonly IVideoGamesGetterByIdRepository _videoGamesGetterByIdRepository;
        private readonly IVideoGamePlatformAvailabilityDeleterRepository _videoGamePlatformAvailabilityDeleterRepository;

        public VideoGamesUpdaterService(IVideoGamesUpdaterRepository videoGamesUpdaterRepository, IVideoGamesGetterByIdRepository videoGamesGetterByIdRepository, IVideoGamePlatformAvailabilityDeleterRepository videoGamePlatformAvailabilityDeleterRepository)
        {
            _videoGamesUpdaterRepository = videoGamesUpdaterRepository;
            _videoGamesGetterByIdRepository = videoGamesGetterByIdRepository;
            _videoGamePlatformAvailabilityDeleterRepository = videoGamePlatformAvailabilityDeleterRepository;
        }

        public async Task<VideoGameResponse> UpdateVideoGame(VideoGameUpdateRequest? videoGameUpdateRequest)
        {
            // validations
            if (videoGameUpdateRequest == null)
                throw new ArgumentNullException(nameof(videoGameUpdateRequest));

            ValidationHelper.ModelValidation(videoGameUpdateRequest);

            VideoGame? videoGameById = await _videoGamesGetterByIdRepository.GetVideoGameById(videoGameUpdateRequest.Id);
            if (videoGameById == null)
                throw new VideoGameNotFoundException("Video game not found!");

            videoGameById.Title = videoGameUpdateRequest.Title ?? string.Empty;
            videoGameById.Genre = videoGameUpdateRequest.Genre.ToString() ?? string.Empty;
            videoGameById.ReleaseDate = videoGameUpdateRequest.ReleaseDate;
            videoGameById.Publisher = videoGameUpdateRequest.Publisher;
            videoGameById.IsMultiplayer = videoGameUpdateRequest.IsMultiplayer;
            videoGameById.IsCoop = videoGameUpdateRequest.IsCoop;

            var newPlatforms = videoGameUpdateRequest.VideoGamePlatformIds;

            var oldPlatforms = videoGameById.VideoGamePlatformAvailability?.ToList();

            // Clear all previous platforms prior to adding new platforms
            videoGameById.VideoGamePlatformAvailability?.Clear();

            if (newPlatforms != null)
            {
                foreach (var platformId in newPlatforms)
                {
                    var videoGamePlatformAvailability = new VideoGamePlatformAvailability()
                    {
                        VideoGameId = videoGameById!.Id,
                        VideoGamePlatformId = platformId
                    };

                    videoGameById.VideoGamePlatformAvailability?.Add(videoGamePlatformAvailability);
                }
            }

            // Get and delete any video game platforms that are not included in the newly updated platforms list
            if (oldPlatforms != null)
            {
                foreach (var platform in oldPlatforms)
                {
                    await _videoGamePlatformAvailabilityDeleterRepository.DeleteVideoGamePlatformAvailabilityById(platform.Id);
                }
            }

            await _videoGamesUpdaterRepository.UpdateVideoGame(videoGameById!);

            return videoGameById!.ToVideoGameResponse();
        }
    }
}
