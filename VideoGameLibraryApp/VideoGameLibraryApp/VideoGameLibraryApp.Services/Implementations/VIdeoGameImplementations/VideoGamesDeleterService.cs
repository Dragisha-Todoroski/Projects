using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs;

namespace VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations
{
    public class VideoGamesDeleterService : IVideoGamesDeleterService
    {
        private readonly IVideoGamesDeleterRepository _videoGamesDeleterRepository;
        private readonly IVideoGamesGetterByIdRepository _videoGamesGetterByIdRepository;

        public VideoGamesDeleterService(IVideoGamesDeleterRepository videoGamesDeleterRepository, IVideoGamesGetterByIdRepository videoGamesGetterByIdRepository)
        {
            _videoGamesDeleterRepository = videoGamesDeleterRepository;
            _videoGamesGetterByIdRepository = videoGamesGetterByIdRepository;
        }

        public async Task<bool> DeleteVideoGameById(Guid? videoGameId)
        {
            // validations
            if (videoGameId == null)
                return false;

            VideoGame? videoGame = await _videoGamesGetterByIdRepository.GetVideoGameById(videoGameId.Value);
            if (videoGame == null)
                return false;

            await _videoGamesDeleterRepository.DeleteVideoGameById(videoGameId.Value);

            return true;
        }
    }
}
