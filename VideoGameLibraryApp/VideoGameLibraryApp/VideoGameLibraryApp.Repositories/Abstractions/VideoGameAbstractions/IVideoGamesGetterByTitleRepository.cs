using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;

namespace VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions
{
    public interface IVideoGamesGetterByTitleRepository
    {
        Task<VideoGame?> GetVideoGameByTitle(string title);
    }
}
