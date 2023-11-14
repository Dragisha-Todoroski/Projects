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
using VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesServicesTests
{
    public class VideoGamesDuplicateCheckerServiceTests
    {
        private readonly IVideoGamesDuplicateCheckerService _videoGamesDuplicateCheckerService;

        private readonly Mock<IVideoGamesGetterAllRepository> _videoGamesGetterAllRepositoryMock;
        private readonly IVideoGamesGetterAllRepository _videoGamesGetterAllRepository;

        private readonly IFixture _fixture;

        public VideoGamesDuplicateCheckerServiceTests()
        {
            _videoGamesGetterAllRepositoryMock = new Mock<IVideoGamesGetterAllRepository>();
            _videoGamesGetterAllRepository = _videoGamesGetterAllRepositoryMock.Object;

            _videoGamesDuplicateCheckerService = new VideoGamesDuplicateCheckerService(_videoGamesGetterAllRepository);

            _fixture = new Fixture();
        }

        #region CheckForDuplicateTitle

        // Test should throw ArgumentNullException if argument is not supplied

        [Fact]

        public async Task CheckForDuplicateTitle_NullArgument_ReturnsArgumentNullException()
        {
            // Arrange
            VideoGameAddRequest? videoGameAddRequest = null;

            // Act
            var action = async () =>
            {
                await _videoGamesDuplicateCheckerService.CheckForDuplicateTitle(videoGameAddRequest?.Title);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }



        // Test should throw DuplicateVideoGameTitleException if duplicate property Title already exists

        [Fact]

        public async Task CheckForDuplicateTitle_DuplicateFound_ThrowsDuplicateVideoGameTitleException()
        {
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture.Create<VideoGameAddRequest>();

            List<VideoGame> videoGamesList = _fixture
                .Build<VideoGame>()
                .With(x => x.Title, videoGameAddRequest.Title)
                .Without(x => x.VideoGamePlatformAvailability)
                .CreateMany().ToList();

            _videoGamesGetterAllRepositoryMock
                .Setup(x => x.GetAllVideoGames())
                .ReturnsAsync(videoGamesList);

            // Act
            var action = async () =>
            {
                await _videoGamesDuplicateCheckerService.CheckForDuplicateTitle(videoGameAddRequest.Title);
            };

            // Assert
            await action.Should().ThrowAsync<DuplicateVideoGameTitleException>();
        }



        // Test should not throw DuplicateVideoGameTitleException if property Title is unique

        [Fact]

        public async Task CheckForDuplicateTitle_DuplicateNotFound_DoesNotThrowDuplicateVideoGameTitleException()
        {
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture.Create<VideoGameAddRequest>();

            List<VideoGame> videoGamesList = _fixture
                .Build<VideoGame>()
                .Without(x => x.VideoGamePlatformAvailability)
                .CreateMany().ToList();

            _videoGamesGetterAllRepositoryMock
                .Setup(x => x.GetAllVideoGames())
                .ReturnsAsync(videoGamesList);

            // Act
            var action = async () =>
            {
                await _videoGamesDuplicateCheckerService.CheckForDuplicateTitle(videoGameAddRequest.Title);
            };

            // Assert
            await action.Should().NotThrowAsync<DuplicateVideoGameTitleException>();
        }

        #endregion
    }
}
