using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Exceptions.CustomExceptions;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;

namespace VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations
{
    public class VideoGamesDuplicateCheckerService : IVideoGamesDuplicateCheckerService
    {
        private readonly IVideoGamesGetterAllRepository _videoGamesGetterAllRepository;

        public VideoGamesDuplicateCheckerService(IVideoGamesGetterAllRepository videoGamesGetterAllRepository)
        {
            _videoGamesGetterAllRepository = videoGamesGetterAllRepository;
        }

        public async Task CheckForDuplicateTitle(string? title)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            var allVideoGames = await _videoGamesGetterAllRepository.GetAllVideoGames();
            if (allVideoGames.Any(x => x.Title == title))
            {
                throw new DuplicateVideoGameTitleException("Title already exists!");
            }
        }
    }
}
