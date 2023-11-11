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
    public class VideoGamesUpdaterService : IVideoGamesUpdaterService
    {
        private readonly IVideoGamesUpdaterRepository _videoGamesUpdaterRepository;
        private readonly IVideoGamesGetterByIdRepository _videoGamesGetterByIdRepository;

        public VideoGamesUpdaterService(IVideoGamesUpdaterRepository videoGamesUpdaterRepository, IVideoGamesGetterByIdRepository videoGamesGetterByIdRepository)
        {
            _videoGamesUpdaterRepository = videoGamesUpdaterRepository;
            _videoGamesGetterByIdRepository = videoGamesGetterByIdRepository;
        }

        public async Task<VideoGameResponse> UpdateVideoGame(VideoGameUpdateRequest? videoGameUpdateRequest)
        {
            // validations
            if (videoGameUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(videoGameUpdateRequest));
            }

            ValidationHelper.ModelValidation(videoGameUpdateRequest);

            VideoGame? videoGameById = await _videoGamesGetterByIdRepository.GetVideoGameById(videoGameUpdateRequest.Id);
            if (videoGameById == null)
                throw new VideoGameNotFoundException("Video game not found!");

            videoGameById.Title = videoGameUpdateRequest.Title;
            videoGameById.Genre = videoGameUpdateRequest.Genre.ToString();
            videoGameById.ReleaseDate = videoGameUpdateRequest.ReleaseDate;
            videoGameById.Publisher = videoGameUpdateRequest.Publisher;
            videoGameById.IsMultiplayer = videoGameUpdateRequest.IsMultiplayer;
            videoGameById.IsCoop = videoGameUpdateRequest.IsCoop;

            await _videoGamesUpdaterRepository.UpdateVideoGame(videoGameById);

            return videoGameById.ToVideoGameResponse();
        }
    }
}
