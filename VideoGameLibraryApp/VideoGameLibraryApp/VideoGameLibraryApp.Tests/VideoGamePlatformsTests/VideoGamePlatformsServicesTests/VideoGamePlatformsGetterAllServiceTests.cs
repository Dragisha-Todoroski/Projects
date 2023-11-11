using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGamePlatformAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGamePlatformAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGamePlatformDTOs;
using VideoGameLibraryApp.Services.Implementations.VIdeoGamePlatformImplementations;

namespace VideoGameLibraryApp.Tests.VideoGamePlatformsTests.VideoGamePlatformsServicesTests
{
    public class VideoGamePlatformsGetterAllServiceTests
    {
        private readonly IVideoGamePlatformsGetterAllService _videoGamePlatformsGetterAllService;

        private readonly Mock<IVideoGamePlatformsGetterAllRepository> _videoGamePlatformsGetterAllRepositoryMock;
        private readonly IVideoGamePlatformsGetterAllRepository _videoGamePlatformsGetterAllRepository;

        private readonly IFixture _fixture;

        public VideoGamePlatformsGetterAllServiceTests()
        {
            _videoGamePlatformsGetterAllRepositoryMock = new Mock<IVideoGamePlatformsGetterAllRepository>();
            _videoGamePlatformsGetterAllRepository = _videoGamePlatformsGetterAllRepositoryMock.Object;

            _videoGamePlatformsGetterAllService = new VideoGamePlatformsGetterAllService(_videoGamePlatformsGetterAllRepository);

            _fixture = new Fixture();
        }

        #region GetAllVideoGamePlatforms

        // Test should return empty list if nothing is fetched

        [Fact]

        public async Task GetAllVideoGamePlatforms_WhenNoVideoGamePlatformsFound_ReturnsEmptyList()
        {
            // Arrange
            List<VideoGamePlatform> videoGamePlatforms = new List<VideoGamePlatform>();

            _videoGamePlatformsGetterAllRepositoryMock
                .Setup(x => x.GetAllVideoGamePlatforms())
                .ReturnsAsync(videoGamePlatforms);

            // Act
            List<VideoGamePlatformResponse> videoGamePlatformResponse = await _videoGamePlatformsGetterAllService.GetAllVideoGamePlatforms();

            // Assert
            videoGamePlatformResponse.Should().BeEmpty();
        }



        // Test should return list of video game platforms if one or more are fetched

        [Fact]

        public async Task GetAllVideoGamePlatforms_WhenVideoGamePlatformsFound_ReturnsListOfVideoGamePlatforms()
        {
            // Arrange
            List<VideoGamePlatform> videoGamePlatforms = _fixture
                .Build<VideoGamePlatform>()
                .Without(x => x.VideoGamePlatformAvailability)
                .CreateMany().ToList();

            List<VideoGamePlatformResponse> videoGamePlatformResponseExpected = videoGamePlatforms.Select(x => x.ToVideoGamePlatformResponse()).ToList();

            _videoGamePlatformsGetterAllRepositoryMock
               .Setup(x => x.GetAllVideoGamePlatforms())
               .ReturnsAsync(videoGamePlatforms);

            // Act
            List<VideoGamePlatformResponse> videoGamePlatformsResponseActual = await _videoGamePlatformsGetterAllService.GetAllVideoGamePlatforms();

            // Assert
            videoGamePlatformsResponseActual.Should().BeEquivalentTo(videoGamePlatformResponseExpected);
        }

        #endregion
    }
}
