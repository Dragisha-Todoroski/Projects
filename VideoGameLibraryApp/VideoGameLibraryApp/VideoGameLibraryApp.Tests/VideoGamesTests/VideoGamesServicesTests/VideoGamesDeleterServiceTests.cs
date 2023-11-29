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
using VideoGameLibraryApp.Services.DTOs;
using VideoGameLibraryApp.Services.Implementations;
using VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesServicesTests
{
    public class VideoGamesDeleterServiceTests
    {
        private readonly IVideoGamesDeleterService _videoGamesDeleterService;

        private readonly Mock<IVideoGamesDeleterRepository> _videoGamesDeleterRepositoryMock;
        private readonly IVideoGamesDeleterRepository _videoGamesDeleterRepository;

        private readonly Mock<IVideoGamesGetterByIdRepository> _videoGamesGetterByIdRepositoryMock;
        private readonly IVideoGamesGetterByIdRepository _videoGamesGetterByIdRepository;

        private readonly IFixture _fixture;

        public VideoGamesDeleterServiceTests()
        {
            _videoGamesDeleterRepositoryMock = new Mock<IVideoGamesDeleterRepository>();
            _videoGamesDeleterRepository = _videoGamesDeleterRepositoryMock.Object;

            _videoGamesGetterByIdRepositoryMock = new Mock<IVideoGamesGetterByIdRepository>();
            _videoGamesGetterByIdRepository = _videoGamesGetterByIdRepositoryMock.Object;

            _videoGamesDeleterService = new VideoGamesDeleterService(_videoGamesDeleterRepository, _videoGamesGetterByIdRepository);

            _fixture = new Fixture();
        }

        #region DeleteVideoGameById

        // Test should return false if video game Id is null

        [Fact]

        public async Task DeleteVideoGameById_NullId_ReturnsFalse()
        {
            // Arrange
            Guid? videoGameId = null;

            // Act
            bool isDeleted = await _videoGamesDeleterService.DeleteVideoGameById(videoGameId);

            // Assert
            isDeleted.Should().BeFalse();
        }



        // Test should return false if video game Id is not found

        [Fact]

        public async Task DeleteVideoGameById_IncorrectId_ReturnsFalse()
        {
            // Arrange
            Guid? videoGameId = Guid.NewGuid();

            // Act
            bool isDeleted = await _videoGamesDeleterService.DeleteVideoGameById(videoGameId);

            // Assert
            isDeleted.Should().BeFalse();
        }



        // Test should return true if a correct video game Id is supplied

        [Fact]

        public async Task DeleteVideoGameById_CorrectId_ReturnsTrue()
        {
            // Arrange
            VideoGame videoGame = _fixture
                .Build<VideoGame>()
                .Without(x => x.VideoGamePlatformAvailability)
                .Without(x => x.User)
                .Create();

            Guid videoGameId = videoGame.Id;

            _videoGamesDeleterRepositoryMock
                .Setup(x => x.DeleteVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _videoGamesGetterByIdRepositoryMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGame);

            // Act
            bool isDeleted = await _videoGamesDeleterService.DeleteVideoGameById(videoGameId);

            // Assert
            isDeleted.Should().BeTrue();
        }

        #endregion
    }
}