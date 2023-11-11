using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;

namespace VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions
{
    public interface IVideoGamesGetterByIdService
    {
        Task<VideoGameResponse?> GetVideoGameById(Guid? videoGameId);
    }
}
