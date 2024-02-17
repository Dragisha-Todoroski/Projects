using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;

namespace VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations
{
    public class VideoGamesFilterService : IVideoGamesFilterService
    {
        public List<VideoGameResponse> GetFilteredVideoGames(string searchBy, string? searchString)
        {
            throw new NotImplementedException();
        }
    }
}
