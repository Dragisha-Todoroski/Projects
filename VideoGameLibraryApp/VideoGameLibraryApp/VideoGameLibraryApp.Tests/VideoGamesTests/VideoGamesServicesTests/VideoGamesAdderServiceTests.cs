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
    public class VideoGamesAdderServiceTests
    {
        private readonly IVideoGamesAdderService _videoGamesAdderService;

        private readonly Mock<IVideoGamesAdderRepository> _videoGamesAdderRepositoryMock;
        private readonly IVideoGamesAdderRepository _videoGamesAdderRepository;

        private readonly IFixture _fixture;

        public VideoGamesAdderServiceTests()
        {
            _videoGamesAdderRepositoryMock = new Mock<IVideoGamesAdderRepository>();
            _videoGamesAdderRepository = _videoGamesAdderRepositoryMock.Object;

            _videoGamesAdderService = new VideoGamesAdderService(_videoGamesAdderRepository);

            _fixture = new Fixture();
        }

        #region AddVideoGame

        // Test should return ArgumentNullException if argument is null

        [Fact]

        public async Task AddVideoGame_NullVideoGameArgument_ReturnsArgumentNullException()
        {
            // Arrange
            VideoGameAddRequest? videoGameAddRequest = null;

            // Act
            var action = async () =>
            {
                await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }


        // Test should return ArgumentException if property Title is null

        [Fact]

        public async Task AddVideoGame_NullTitle_ReturnsArgumentException()
        {
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture
                .Build<VideoGameAddRequest>()
                .With(x => x.Title, null as string)
                .Create();

            // Act
            var action = async () =>
            {
                await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // Test should return ArgumentException if property Title is longer than 100 characters long

        [Fact]

        public async Task AddVideoGame_TitleLengthOver100Characters_ReturnsArgumentException()
        {
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture
                .Build<VideoGameAddRequest>()
                .With(x => x.Title, new string('a', 101))
                .Create();

            // Act
            var action = async () =>
            {
                await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }



        // Test should return ArgumentException if property Genre is null

        [Fact]

        public async Task AddVideoGame_NullGenre_ReturnsArgumentException()
        {
            VideoGameGenre? videoGameGenreNull = null;
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture
                .Build<VideoGameAddRequest>()
                .With(x => x.Genre, videoGameGenreNull)
                .Create();

            // Act
            var action = async () =>
            {
                await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // Test should return ArgumentException if property Publisher is longer than 50 characters long

        [Fact]

        public async Task AddVideoGame_PublisherLengthOver50Characters_ReturnsArgumentException()
        {
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture
                .Build<VideoGameAddRequest>()
                .With(x => x.Publisher, new string('a', 51))
                .Create();

            // Act
            var action = async () =>
            {
                await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // Test should successfully insert VideoGame into DB with appropriate Id

        [Fact]

        public async Task AddVideoGame_VideoGameWithCorrectValues_SuccessfulInsertion()
        {
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture.Create<VideoGameAddRequest>();

            VideoGame videoGame = videoGameAddRequest.ToVideoGame();
            VideoGameResponse videoGameResponseExpected = videoGame.ToVideoGameResponse();

            List<VideoGame> videoGamesList = _fixture
                .Build<VideoGame>()
                .Without(x => x.VideoGamePlatformAvailability)
                .CreateMany().ToList();

            _videoGamesAdderRepositoryMock
                .Setup(x => x.AddVideoGame(It.IsAny<VideoGame>()))
                .ReturnsAsync(videoGame);

            // Act
            VideoGameResponse videoGameResponseActual = await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);
            
            videoGameResponseExpected.Id = videoGameResponseActual.Id;

            // Assert
            videoGameResponseActual.Id.Should().NotBe(Guid.Empty);
            videoGameResponseActual.Should().Be(videoGameResponseExpected);
        }

        #endregion
    }
}
