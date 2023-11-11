using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;

namespace VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions
{
    public interface IVideoGamesUpdaterService
    {
        Task<VideoGameResponse> UpdateVideoGame(VideoGameUpdateRequest? videoGameUpdateRequest);
    }
}
