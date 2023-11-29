using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;
using VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesServicesTests
{
    public class VideoGamesGetterByTitleServiceTests
    {
        private readonly IVideoGamesGetterByTitleService _videoGamesGetterByTitleService;

        private readonly Mock<IVideoGamesGetterByTitleRepository> _videoGamesGetterByTitleRepositoryMock;
        private readonly IVideoGamesGetterByTitleRepository _videoGamesGetterByTitleRepository;

        private readonly IFixture _fixture;

        public VideoGamesGetterByTitleServiceTests()
        {
            _videoGamesGetterByTitleRepositoryMock = new Mock<IVideoGamesGetterByTitleRepository>();
            _videoGamesGetterByTitleRepository = _videoGamesGetterByTitleRepositoryMock.Object;

            _videoGamesGetterByTitleService = new VideoGamesGetterByTitleService(_videoGamesGetterByTitleRepository);

            _fixture = new Fixture();
        }


        #region GetVideoGameByTitle

        // Test should return null if argument Title is null

        [Fact]

        public async Task GetVideoGameByTitle_NullTitle_ReturnsNull()
        {
            // Arrange
            string? title = null;

            // Act
            VideoGameResponse? videoGameResponseByTitle = await _videoGamesGetterByTitleService.GetVideoGameByTitle(title);

            // Assert
            videoGameResponseByTitle.Should().BeNull();
        }



        // Test should return null if video game Title is not found

        [Fact]

        public async Task GetVideoGameByTitle_IncorrectTitle_ReturnsNull()
        {
            // Arrange
            VideoGame? videoGame = null;

            string title = _fixture.Create<string>();

            _videoGamesGetterByTitleRepositoryMock
                .Setup(x => x.GetVideoGameByTitle(It.IsAny<string>()))
                .ReturnsAsync(videoGame);

            // Act
            VideoGameResponse? videoGameResponseByTitle = await _videoGamesGetterByTitleService.GetVideoGameByTitle(title);

            // Assert
            videoGameResponseByTitle.Should().BeNull();
        }



        // Test should return appropriate video game if a correct video game Title is supplied

        [Fact]

        public async Task GetVideoGameByTitle_CorrectTitle_ReturnsRelatedVideoGame()
        {
            // Arrange
            VideoGame? videoGame = _fixture
                .Build<VideoGame>()
                .Without(x => x.VideoGamePlatformAvailability)
                .Without(x => x.User)
                .Create();

            VideoGameResponse videoGameResponseExpected = videoGame.ToVideoGameResponse();

            _videoGamesGetterByTitleRepositoryMock
                .Setup(x => x.GetVideoGameByTitle(It.IsAny<string>()))
                .ReturnsAsync(videoGame);

            // Act
            VideoGameResponse? videoGameResponseByActual = await _videoGamesGetterByTitleService.GetVideoGameByTitle(videoGame.Title);

            // Assert
            videoGameResponseByActual.Should().BeEquivalentTo(videoGameResponseExpected);
        }

        #endregion
    }
}
