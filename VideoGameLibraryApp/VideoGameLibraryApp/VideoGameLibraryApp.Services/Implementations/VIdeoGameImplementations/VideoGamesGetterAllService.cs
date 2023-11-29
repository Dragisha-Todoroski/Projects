using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;

namespace VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations
{
    public class VideoGamesGetterAllService : IVideoGamesGetterAllService
    {
        private readonly IVideoGamesGetterAllRepository _videoGamesGetterAllRepository;

        public VideoGamesGetterAllService(IVideoGamesGetterAllRepository videoGamesGetterAllRepository)
        {
            _videoGamesGetterAllRepository = videoGamesGetterAllRepository;
        }

        public async Task<List<VideoGameResponse>> GetAllVideoGames()
        {
            List<VideoGame> videoGames = await _videoGamesGetterAllRepository.GetAllVideoGames();

            return videoGames.Select(x => x.ToVideoGameResponse()).ToList();
        }
    }
}
