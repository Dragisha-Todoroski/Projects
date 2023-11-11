using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Controllers;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Exceptions.CustomExceptions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGamePlatformAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;
using VideoGameLibraryApp.Services.DTOs.VideoGamePlatformDTOs;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesControllersTests
{
    public class VideoGamesControllerTests
    {
        private readonly VideoGamesController _videoGamesController;

        private readonly Mock<IVideoGamesGetterAllService> _videoGamesGetterAllServiceMock;
        private readonly IVideoGamesGetterAllService _videoGamesGetterAllService;

        private readonly Mock<IVideoGamesAdderService> _videoGamesAdderServiceMock;
        private readonly IVideoGamesAdderService _videoGamesAdderService;

        private readonly Mock<IVideoGamePlatformsGetterAllService> _videoGamePlatformsGetterAllServiceMock;
        private readonly IVideoGamePlatformsGetterAllService _videoGamePlatformsGetterAllService;

        private readonly IFixture _fixture;

        public VideoGamesControllerTests()
        {
            _videoGamesGetterAllServiceMock = new Mock<IVideoGamesGetterAllService>();
            _videoGamesGetterAllService = _videoGamesGetterAllServiceMock.Object;

            _videoGamesAdderServiceMock = new Mock<IVideoGamesAdderService>();
            _videoGamesAdderService = _videoGamesAdderServiceMock.Object;

            _videoGamePlatformsGetterAllServiceMock = new Mock<IVideoGamePlatformsGetterAllService>();
            _videoGamePlatformsGetterAllService = _videoGamePlatformsGetterAllServiceMock.Object;

            _videoGamesController = new VideoGamesController(_videoGamesGetterAllService, _videoGamesAdderService, _videoGamePlatformsGetterAllService);

            _fixture = new Fixture();
        }


        #region Index

        // Test should return ViewResult with appropriate Model

        [Fact]

        public async Task Index_ReturnsViewResultWithCorrectModel()
        {
            // Arrange
            List<VideoGameResponse> videoGameResponseList = _fixture
                .Build<VideoGameResponse>()
                .Without(x => x.VideoGamePlatformAvailability)
                .CreateMany().ToList();

            _videoGamesGetterAllServiceMock
                .Setup(x => x.GetAllVideoGames())
                .ReturnsAsync(videoGameResponseList);

            // Act
            IActionResult result = await _videoGamesController.Index();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeAssignableTo<IEnumerable<VideoGameResponse>>();

            viewResult.Model.Should().Be(videoGameResponseList);
        }

        #endregion


        #region Create

        // Test should return ViewResult with appropriate Model in the case of model errors

        [Fact]

        public async Task Create_ActivateHttpGetMethod_ReturnsViewResultWithCorrectModel()
        {
            // Assert
            List<VideoGamePlatformResponse> videoGamePlatformResponseList = _fixture.CreateMany<VideoGamePlatformResponse>().ToList();

            _videoGamePlatformsGetterAllServiceMock
                .Setup(x => x.GetAllVideoGamePlatforms())
                .ReturnsAsync(videoGamePlatformResponseList);

            // Act
            IActionResult result = await _videoGamesController.Create();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
        }



        // Test should return ViewResult with appropriate Model in the case of model errors and DuplicateVideoGameTitleException

        [Fact]

        public async Task Create_IfModelErrorsInHttpPostMethodWithDuplicateVideoGameTitle_ReturnsViewResultWithCorrectModel()
        {
            // Assert
            VideoGameAddRequest videoGameAddRequest = _fixture.Create<VideoGameAddRequest>();

            List<VideoGamePlatformResponse> videoGamePlatformResponseList = _fixture.CreateMany<VideoGamePlatformResponse>().ToList();

            _videoGamesAdderServiceMock
                .Setup(x => x.CheckForDuplicateTitle(It.IsAny<string>()))
                .ThrowsAsync(new DuplicateVideoGameTitleException());

            _videoGamePlatformsGetterAllServiceMock
                .Setup(x => x.GetAllVideoGamePlatforms())
                .ReturnsAsync(videoGamePlatformResponseList);

            // Act
            _videoGamesController.ModelState.AddModelError(nameof(videoGameAddRequest.Title), "Title can't be longer than 100 characters!");

            IActionResult result = await _videoGamesController.Create(videoGameAddRequest);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeAssignableTo<VideoGameAddRequest>();

            viewResult.Model.Should().Be(videoGameAddRequest);
        }



        // Test should return ViewResult with appropriate Model in the case of model errors

        [Fact]

        public async Task Create_IfModelErrorsInHttpPostMethod_ReturnsViewResultWithCorrectModel()
        {
            // Assert
            VideoGameAddRequest videoGameAddRequest = _fixture.Create<VideoGameAddRequest>();

            List<VideoGamePlatformResponse> videoGamePlatformResponseList = _fixture.CreateMany<VideoGamePlatformResponse>().ToList();

            _videoGamesAdderServiceMock
                .Setup(x => x.CheckForDuplicateTitle(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _videoGamePlatformsGetterAllServiceMock
                .Setup(x => x.GetAllVideoGamePlatforms())
                .ReturnsAsync(videoGamePlatformResponseList);

            // Act
            _videoGamesController.ModelState.AddModelError(nameof(videoGameAddRequest.Title), "Title can't be longer than 100 characters!");

            IActionResult result = await _videoGamesController.Create(videoGameAddRequest);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeAssignableTo<VideoGameAddRequest>();

            viewResult.Model.Should().Be(videoGameAddRequest);
        }



        // Test should return RedirectToActionResult to Index action method in the case of no model errors

        [Fact]

        public async Task Create_IfNoModelErrorsInHttpPostMethod_ReturnsRedirectToActionResultToIndex()
        {
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture.Create<VideoGameAddRequest>();

            VideoGameResponse videoGameResponse = videoGameAddRequest.ToVideoGame().ToVideoGameResponse();

            _videoGamesAdderServiceMock
                .Setup(x => x.AddVideoGame(It.IsAny<VideoGameAddRequest>()))
                .ReturnsAsync(videoGameResponse);

            // Act
            IActionResult result = await _videoGamesController.Create(videoGameAddRequest);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            redirectResult.ActionName.Should().Be("Index");
        }

        #endregion
    }
}
