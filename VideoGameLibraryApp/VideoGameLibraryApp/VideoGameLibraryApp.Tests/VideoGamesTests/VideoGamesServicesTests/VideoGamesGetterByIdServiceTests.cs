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
using VideoGameLibraryApp.Services.Implementations;
using VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesServicesTests
{
    public class VideoGamesGetterByIdServiceTests
    {
        private readonly IVideoGamesGetterByIdService _videoGamesGetterByIdService;

        private readonly Mock<IVideoGamesGetterByIdRepository> _videoGamesGetterByIdRepositoryMock;
        private readonly IVideoGamesGetterByIdRepository _videoGamesGetterByIdRepository;

        private readonly IFixture _fixture;

        public VideoGamesGetterByIdServiceTests()
        {
            _videoGamesGetterByIdRepositoryMock = new Mock<IVideoGamesGetterByIdRepository>();
            _videoGamesGetterByIdRepository = _videoGamesGetterByIdRepositoryMock.Object;

            _videoGamesGetterByIdService = new VideoGamesGetterByIdService(_videoGamesGetterByIdRepository);

            _fixture = new Fixture();
        }

        #region GetVideoGameById

        // Test should return null if video game Id is null

        [Fact]

        public async Task GetVideoGameById_NullId_ReturnsNull()
        {
            // Arrange
            Guid? videoGameId = null;

            // Act
            VideoGameResponse? videoGameById = await _videoGamesGetterByIdService.GetVideoGameById(videoGameId);

            // Assert
            videoGameById.Should().BeNull();
        }


        
        // Test should return null if video game Id is not found

        [Fact]

        public async Task GetVideoGameById_IncorrectId_ReturnsNull()
        {
            // Arrange
            VideoGame? videoGame = null;

            Guid videoGameId = Guid.NewGuid();

            _videoGamesGetterByIdRepositoryMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGame);

            // Act
            VideoGameResponse? videoGameById = await _videoGamesGetterByIdService.GetVideoGameById(videoGameId);

            // Assert
            videoGameById.Should().BeNull();
        }



        // Test should return appropriate video game if a correct video game Id is supplied

        [Fact]

        public async Task GetVideoGameById_CorrectId_ReturnsRelatedVideoGame()
        {
            // Arrange
            VideoGame videoGame = _fixture
                .Build<VideoGame>()
                .Without(x => x.VideoGamePlatformAvailability)
                .Without(x => x.User)
                .Create();

            VideoGameResponse videoGameResponseExpected = videoGame.ToVideoGameResponse();

            _videoGamesGetterByIdRepositoryMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGame);

            // Act
            VideoGameResponse? videoGameResponseActual = await _videoGamesGetterByIdService.GetVideoGameById(videoGame.Id);

            // Assert
            videoGameResponseActual.Should().BeEquivalentTo(videoGameResponseExpected);

        }

        #endregion
    }
}