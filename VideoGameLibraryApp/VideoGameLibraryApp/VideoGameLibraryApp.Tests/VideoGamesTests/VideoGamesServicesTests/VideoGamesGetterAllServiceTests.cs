using AutoFixture;
using FluentAssertions;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Repositories.Implementations;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;
using VideoGameLibraryApp.Services.Implementations;
using VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesServicesTests
{
    public class VideoGamesGetterAllServiceTests
    {
        private readonly IVideoGamesGetterAllService _videoGamesGetterAllService;

        private readonly Mock<IVideoGamesGetterAllRepository> _videoGamesGetterAllRepositoryMock;
        private readonly IVideoGamesGetterAllRepository _videoGamesGetterAllRepository;

        private readonly IFixture _fixture;

        public VideoGamesGetterAllServiceTests()
        {
            _videoGamesGetterAllRepositoryMock = new Mock<IVideoGamesGetterAllRepository>();
            _videoGamesGetterAllRepository = _videoGamesGetterAllRepositoryMock.Object;

            _videoGamesGetterAllService = new VideoGamesGetterAllService(_videoGamesGetterAllRepository);

            _fixture = new Fixture();
        }

        #region GetAllVideoGames

        // Test should return empty list if nothing is fetched

        [Fact]

        public async Task GetAllVideoGames_WhenNoVideoGamesFound_ReturnsEmptyList()
        {
            // Arrange
            List<VideoGame> videoGames = new List<VideoGame>();

            _videoGamesGetterAllRepositoryMock
                .Setup(x => x.GetAllVideoGames())
                .ReturnsAsync(videoGames);

            // Act
            List<VideoGameResponse> videoGameResponseList = await _videoGamesGetterAllService.GetAllVideoGames();

            // Assert
            videoGameResponseList.Should().BeEmpty();
        }


        // Test should return list of video games if one or more are fetched

        [Fact]

        public async Task GetAllVideoGames_WhenVideoGamesFound_ReturnsListOfVideoGames()
        {
            // Arrange
            List<VideoGame> videoGames = _fixture
                .Build<VideoGame>()
                .Without(x => x.VideoGamePlatformAvailability)
                .CreateMany().ToList();

            List<VideoGameResponse> videoGameResponseListExpected = videoGames.Select(x => x.ToVideoGameResponse()).ToList();

            _videoGamesGetterAllRepositoryMock
                .Setup(x => x.GetAllVideoGames())
                .ReturnsAsync(videoGames);

            // Act
            List<VideoGameResponse> videoGameResponseListActual = await _videoGamesGetterAllService.GetAllVideoGames();

            // Assert
            videoGameResponseListActual.Should().BeEquivalentTo(videoGameResponseListExpected);
        }
        

        #endregion
    }
}
