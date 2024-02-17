using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;
using VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesServicesTests
{
    public class VideoGamesFilterServiceTests
    {
        private readonly IVideoGamesFilterService _videoGamesFilterService;

        //private readonly Mock<IVideoGamesFilterRepository> _videoGamesFilterRepositoryMock;
        //private readonly IVideoGamesFilterRepository _videoGamesFilterRepository;

        private readonly IFixture _fixture;

        public VideoGamesFilterServiceTests()
        {
            //_videoGamesFilterRepositoryMock = new Mock<IVideoGamesGetterAllRepository>();
            //_videoGamesFilterRepository = _videoGamesGetterAllRepositoryMock.Object;

            _videoGamesFilterService = new VideoGamesFilterService();

            _fixture = new Fixture();
        }


        // If searchString is empty and searchBy is Title, it should simply return all video games

        [Fact]

        public async Task GetFilteredVideoGames_EmptySearchString_ReturnsAllVideoGames()
        {
            // Arrange
            List<VideoGameResponse> videoGamesList = _fixture.CreateMany<VideoGameResponse>().ToList();

            // Act

        }
    }
}
