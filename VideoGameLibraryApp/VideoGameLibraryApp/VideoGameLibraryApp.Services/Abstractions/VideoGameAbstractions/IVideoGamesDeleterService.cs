﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Services.DTOs;

namespace VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions
{
    public interface IVideoGamesDeleterService
    {
        Task<bool> DeleteVideoGameById(Guid? videoGameId);
    }
}
