using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Exceptions.CustomExceptions;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;
using VideoGameLibraryApp.Services.Enums;
using VideoGameLibraryApp.Services.Implementations;
using VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesServicesTests
{
    public class VideoGamesUpdaterServiceTests
    {
        private readonly IVideoGamesUpdaterService _videoGamesUpdaterService;

        private readonly Mock<IVideoGamesUpdaterRepository> _videoGamesUpdaterRepositoryMock;
        private readonly IVideoGamesUpdaterRepository _videoGamesUpdaterRepository;

        private readonly Mock<IVideoGamesGetterByIdRepository> _videoGamesGetterByIdRepositoryMock;
        private readonly IVideoGamesGetterByIdRepository _videoGamesGetterByIdRepository;

        private readonly IFixture _fixture;

        public VideoGamesUpdaterServiceTests()
        {
            _videoGamesUpdaterRepositoryMock = new Mock<IVideoGamesUpdaterRepository>();
            _videoGamesUpdaterRepository = _videoGamesUpdaterRepositoryMock.Object;

            _videoGamesGetterByIdRepositoryMock = new Mock<IVideoGamesGetterByIdRepository>();
            _videoGamesGetterByIdRepository = _videoGamesGetterByIdRepositoryMock.Object;

            _videoGamesUpdaterService = new VideoGamesUpdaterService(_videoGamesUpdaterRepository, _videoGamesGetterByIdRepository);

            _fixture = new Fixture();
        }

        #region UpdateVideoGame

        // Test should return ArgumentNullException if argument is null

        [Fact]

        public async Task UpdateVideoGame_NullVideoGameArgument_ReturnsArgumentNullException()
        {
            // Arrange
            VideoGameUpdateRequest? videoGameUpdateRequest = null;

            // Act
            var action = async () =>
            {
                await _videoGamesUpdaterService.UpdateVideoGame(videoGameUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }



        // Test should return ArgumentException if property Title is null

        [Fact]

        public async Task UpdateVideoGame_NullTitle_ReturnsArgumentException()
        {
            // Arrange
            VideoGameUpdateRequest videoGameUpdateRequest = _fixture
                .Build<VideoGameUpdateRequest>()
                .With(x => x.Title, null as string)
                .Create();

            // Act
            var action = async () =>
            {
                await _videoGamesUpdaterService.UpdateVideoGame(videoGameUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }



        // Test should return ArgumentException if property Title is longer than 100 characters long

        [Fact]

        public async Task UpdateVideoGame_TitleLengthOver100Characters_ReturnsArgumentException()
        {
            // Arrange
            VideoGameUpdateRequest videoGameUpdateRequest = _fixture
                .Build<VideoGameUpdateRequest>()
                .With(x => x.Title, new string('a', 101))
                .Create();

            // Act
            var action = async () =>
            {
                await _videoGamesUpdaterService.UpdateVideoGame(videoGameUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }



        // Test should return ArgumentException if property Genre is null

        [Fact]

        public async Task UpdateVideoGame_NullGenre_ReturnsArgumentException()
        {
            VideoGameGenre? videoGameGenreNull = null;
            // Arrange
            VideoGameUpdateRequest videoGameUpdateRequest = _fixture
                .Build<VideoGameUpdateRequest>()
                .With(x => x.Genre, videoGameGenreNull)
                .Create();

            // Act
            var action = async () =>
            {
                await _videoGamesUpdaterService.UpdateVideoGame(videoGameUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }



        // Test should return ArgumentException if property Publisher is longer than 50 characters long

        [Fact]

        public async Task UpdateVideoGame_PublisherLengthOver50Characters_ReturnsArgumentException()
        {
            // Arrange
            VideoGameUpdateRequest videoGameUpdateRequest = _fixture
                .Build<VideoGameUpdateRequest>()
                .With(x => x.Publisher, new string('a', 51))
                .Create();

            // Act
            var action = async () =>
            {
                await _videoGamesUpdaterService.UpdateVideoGame(videoGameUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }



        // Test should return VideoGameNotFoundException if video game is not found

        [Fact]

        public async Task UpdateVideoGame_IncorrectId_ReturnsVideoGameNotFoundException()
        {
            // Arrange
            VideoGameUpdateRequest videoGameUpdateRequest = _fixture
                .Build<VideoGameUpdateRequest>()
                .With(x => x.Id, Guid.NewGuid())
                .Create();

            _videoGamesGetterByIdRepositoryMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(null as VideoGame);

            // Act
            var action = async () =>
            {
                await _videoGamesUpdaterService.UpdateVideoGame(videoGameUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<VideoGameNotFoundException>();

        }



        // Test should successfully update existing VideoGame in DB

        [Fact]

        public async Task UpdateVideoGame_VideoGameWithCorrectValues_SuccessfulUpdation()
        {
            // Arrange
            VideoGameUpdateRequest videoGameUpdateRequest = _fixture.Create<VideoGameUpdateRequest>();

            VideoGame videoGame = videoGameUpdateRequest.ToVideoGame();
            VideoGameResponse videoGameResponseExpected = videoGame.ToVideoGameResponse();

            _videoGamesUpdaterRepositoryMock
                .Setup(x => x.UpdateVideoGame(It.IsAny<VideoGame>()))
                .ReturnsAsync(videoGame);

            _videoGamesGetterByIdRepositoryMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGame);

            // Act
            VideoGameResponse videoGameResponseActual = await _videoGamesUpdaterService.UpdateVideoGame(videoGameUpdateRequest);

            // Assert
            videoGameResponseActual.Should().Be(videoGameResponseExpected);
        }

        #endregion

    }
}
