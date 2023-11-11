﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;

namespace VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions
{
    public interface IVideoGamesAdderRepository
    {
        Task<VideoGame> AddVideoGame(VideoGame videoGame);
    }
}
