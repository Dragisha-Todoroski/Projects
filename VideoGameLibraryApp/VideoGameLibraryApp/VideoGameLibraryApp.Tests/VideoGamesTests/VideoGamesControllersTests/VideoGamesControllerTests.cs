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
using VideoGameLibraryApp.Services.Enums;

namespace VideoGameLibraryApp.Tests.VideoGamesTests.VideoGamesControllersTests
{
    public class VideoGamesControllerTests
    {
        private readonly VideoGamesController _videoGamesController;

        private readonly Mock<IVideoGamesGetterAllService> _videoGamesGetterAllServiceMock;
        private readonly IVideoGamesGetterAllService _videoGamesGetterAllService;

        private readonly Mock<IVideoGamesAdderService> _videoGamesAdderServiceMock;
        private readonly IVideoGamesAdderService _videoGamesAdderService;

        private readonly Mock<IVideoGamesDuplicateCheckerService> _videoGamesDuplicateCheckerServiceMock;
        private readonly IVideoGamesDuplicateCheckerService _videoGamesDuplicateCheckerService;

        private readonly Mock<IVideoGamesGetterByIdService> _videoGamesGetterByIdServiceMock;
        private readonly IVideoGamesGetterByIdService _videoGamesGetterByIdService;

        private readonly Mock<IVideoGamePlatformsGetterAllService> _videoGamePlatformsGetterAllServiceMock;
        private readonly IVideoGamePlatformsGetterAllService _videoGamePlatformsGetterAllService;

        private readonly Mock<IVideoGamesUpdaterService> _videoGamesUpdaterServiceMock;
        private readonly IVideoGamesUpdaterService _videoGamesUpdaterService;

        private readonly Mock<IVideoGamesDeleterService> _videoGamesDeleterServiceMock;
        private readonly IVideoGamesDeleterService _videoGamesDeleterService;

        private readonly IFixture _fixture;

        public VideoGamesControllerTests()
        {
            _videoGamesGetterAllServiceMock = new Mock<IVideoGamesGetterAllService>();
            _videoGamesGetterAllService = _videoGamesGetterAllServiceMock.Object;

            _videoGamesAdderServiceMock = new Mock<IVideoGamesAdderService>();
            _videoGamesAdderService = _videoGamesAdderServiceMock.Object;

            _videoGamesDuplicateCheckerServiceMock = new Mock<IVideoGamesDuplicateCheckerService>();
            _videoGamesDuplicateCheckerService = _videoGamesDuplicateCheckerServiceMock.Object;

            _videoGamesGetterByIdServiceMock = new Mock<IVideoGamesGetterByIdService>();
            _videoGamesGetterByIdService = _videoGamesGetterByIdServiceMock.Object;

            _videoGamesUpdaterServiceMock = new Mock<IVideoGamesUpdaterService>();
            _videoGamesUpdaterService = _videoGamesUpdaterServiceMock.Object;

            _videoGamePlatformsGetterAllServiceMock = new Mock<IVideoGamePlatformsGetterAllService>();
            _videoGamePlatformsGetterAllService = _videoGamePlatformsGetterAllServiceMock.Object;

            _videoGamesDeleterServiceMock = new Mock<IVideoGamesDeleterService>();
            _videoGamesDeleterService = _videoGamesDeleterServiceMock.Object;

            _videoGamesController = new VideoGamesController(_videoGamesGetterAllService, _videoGamesGetterByIdService, _videoGamesDuplicateCheckerService, _videoGamesAdderService, _videoGamesUpdaterService, _videoGamePlatformsGetterAllService, _videoGamesDeleterService);

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
                .Without(x => x.VideoGamePlatformAvailabilityIds)
                .CreateMany().ToList();

            _videoGamesGetterAllServiceMock
                .Setup(x => x.GetAllVideoGames())
                .ReturnsAsync(videoGameResponseList);

            // Act
            IActionResult result = await _videoGamesController.Index();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<List<VideoGameResponse>>();

            viewResult.Model.Should().Be(videoGameResponseList);
        }

        #endregion


        #region Create

        // Test should return ViewResult with appropriate Model

        [Fact]

        public async Task Create_ActivateHttpGetMethodSuccessfully_ReturnsViewResultWithCorrectModel()
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

            _videoGamesDuplicateCheckerServiceMock
                .Setup(x => x.CheckForDuplicateTitle(It.IsAny<string>()))
                .ThrowsAsync(new DuplicateVideoGameTitleException());

            // Act
            _videoGamesController.ModelState.AddModelError(nameof(videoGameAddRequest.Title), "Title can't be longer than 100 characters!");

            IActionResult result = await _videoGamesController.Create(videoGameAddRequest);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<VideoGameAddRequest>();

            viewResult.Model.Should().BeEquivalentTo(videoGameAddRequest);
        }



        // Test should return ViewResult with appropriate Model in the case of model errors

        [Fact]

        public async Task Create_IfModelErrorsInHttpPostMethod_ReturnsViewResultWithCorrectModel()
        {
            // Assert
            VideoGameAddRequest videoGameAddRequest = _fixture.Create<VideoGameAddRequest>();

            List<VideoGamePlatformResponse> videoGamePlatformResponseList = _fixture.CreateMany<VideoGamePlatformResponse>().ToList();

            _videoGamesDuplicateCheckerServiceMock
                .Setup(x => x.CheckForDuplicateTitle(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            _videoGamesController.ModelState.AddModelError(nameof(videoGameAddRequest.Title), "Title can't be longer than 100 characters!");

            IActionResult result = await _videoGamesController.Create(videoGameAddRequest);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<VideoGameAddRequest>();

            viewResult.Model.Should().BeEquivalentTo(videoGameAddRequest);
        }



        // Test should return RedirectToActionResult to Index action method in the case of no model errors

        [Fact]

        public async Task Create_IfNoModelErrorsInHttpPostMethod_ReturnsRedirectToActionResultToIndex()
        {
            // Arrange
            VideoGameAddRequest videoGameAddRequest = _fixture.Create<VideoGameAddRequest>();

            VideoGameResponse videoGameResponse = videoGameAddRequest.ToVideoGame().ToVideoGameResponse();

            _videoGamesDuplicateCheckerServiceMock
                .Setup(x => x.CheckForDuplicateTitle(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

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


        #region Edit

        // Test should return RedirectToActionResult to Index action method if no video game is found

        [Fact]

        public async Task Edit_ActivateHttpGetMethodWithNoVideoGameFound_ReturnsRedirectToActionResult()
        {
            // Arrange
            VideoGameResponse? videoGameResponse = null;

            _videoGamesGetterByIdServiceMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGameResponse);

            // Act
            IActionResult result = await _videoGamesController.Edit(Guid.NewGuid());

            // Assert
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            redirectToActionResult.ActionName.Should().Be("Index");
        }



        // Test should return ViewResult with appropriate Model

        [Fact]

        public async Task Edit_ActivateHttpGetMethodSuccessfully_ReturnsViewResultWithCorrectModel()
        {
            // Arrange
            VideoGameResponse videoGameResponse = _fixture
                .Build<VideoGameResponse>()
                .With(x => x.Genre, VideoGameGenre.Action.ToString())
                .Create();

            VideoGameUpdateRequest videoGameUpdateRequest = videoGameResponse.ToVideoGameUpdateRequest();

            List<VideoGamePlatformResponse> videoGamePlatformResponseList = _fixture.CreateMany<VideoGamePlatformResponse>().ToList();

            _videoGamesGetterByIdServiceMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGameResponse);

            _videoGamePlatformsGetterAllServiceMock
                .Setup(x => x.GetAllVideoGamePlatforms())
                .ReturnsAsync(videoGamePlatformResponseList);

            // Act
            IActionResult result = await _videoGamesController.Edit(videoGameResponse.Id);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<VideoGameUpdateRequest>();

            viewResult.Model.Should().BeEquivalentTo(videoGameUpdateRequest);
        }



        // Test should return RedirectToActionResult to Index action method if video game is not found

        [Fact]

        public async Task Edit_ActivateHttpPostMethodWithNoVideoGameFound_ReturnsRedirectToActionResult()
        {
            // Arrange
            VideoGameResponse? videoGameResponse = null;

            VideoGameUpdateRequest? videoGameUpdateRequest = null;

            _videoGamesGetterByIdServiceMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGameResponse);

            // Act
            IActionResult result = await _videoGamesController.Edit(videoGameUpdateRequest);

            // Assert
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            redirectToActionResult.ActionName.Should().Be("Index");
        }



        // Test should return ViewResult with appropriate Model in the case of model errors

        [Fact]

        public async Task Edit_IfModelErrorsInHttpPostMethod_ReturnsViewResultWithCorrectModel()
        {
            // Assert
            VideoGameResponse videoGameResponse = _fixture
               .Build<VideoGameResponse>()
               .With(x => x.Genre, VideoGameGenre.Action.ToString())
               .Create();

            VideoGameUpdateRequest videoGameUpdateRequest = videoGameResponse.ToVideoGameUpdateRequest();

            List<VideoGamePlatformResponse> videoGamePlatformResponseList = _fixture.CreateMany<VideoGamePlatformResponse>().ToList();

            _videoGamesGetterByIdServiceMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGameResponse);

            _videoGamePlatformsGetterAllServiceMock
                .Setup(x => x.GetAllVideoGamePlatforms())
                .ReturnsAsync(videoGamePlatformResponseList);

            // Act
            _videoGamesController.ModelState.AddModelError(nameof(videoGameUpdateRequest.Title), "Title can't be longer than 100 characters!");

            IActionResult result = await _videoGamesController.Edit(videoGameUpdateRequest);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<VideoGameUpdateRequest>();

            viewResult.Model.Should().BeEquivalentTo(videoGameUpdateRequest);
        }



        // Test should return RedirectToActionResult to Index action method in the case of no model errors

       [Fact]

        public async Task Edit_IfNoModelErrorsInHttpPostMethod_ReturnsRedirectToActionResultToIndex()
        {
            // Arrange
            VideoGameUpdateRequest videoGameUpdateRequest = _fixture.Create<VideoGameUpdateRequest>();

            VideoGameResponse videoGameResponse = videoGameUpdateRequest.ToVideoGame().ToVideoGameResponse();

            _videoGamesUpdaterServiceMock
                .Setup(x => x.UpdateVideoGame(It.IsAny<VideoGameUpdateRequest>()))
                .ReturnsAsync(videoGameResponse);

            // Act
            IActionResult result = await _videoGamesController.Edit(videoGameUpdateRequest);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            redirectResult.ActionName.Should().Be("Index");
        }

        #endregion


        #region Delete

        // Test should return RedirectToActionResult to Index action method if no video game is found

        [Fact]

        public async Task Delete_ActivateHttpGetWithNoVideoGamesFound_ReturnsRedirectToActionResult()
        {
            VideoGameResponse? videoGameResponse = null;

            _videoGamesGetterByIdServiceMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGameResponse);

            // Act
            IActionResult result = await _videoGamesController.Delete(Guid.NewGuid());

            // Assert
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            redirectToActionResult.ActionName.Should().Be("Index");
        }



        // Test should return ViewResult with appropriate Model

        [Fact]

        public async Task Delete_ActivateHttpGetMethodSuccessfully_ReturnsViewResultWithCorrectModel()
        {
            // Arrange
            VideoGameResponse videoGameResponse = _fixture
                .Build<VideoGameResponse>()
                .With(x => x.Genre, VideoGameGenre.Action.ToString())
                .Create();

            _videoGamesGetterByIdServiceMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGameResponse);

            // Act
            IActionResult result = await _videoGamesController.Delete(videoGameResponse.Id);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<VideoGameResponse>();

            viewResult.Model.Should().BeEquivalentTo(videoGameResponse);
        }



        // Test should return RedirectToActionResult to Index action method if video game is not found

        [Fact]

        public async Task Delete_ActivateHttpPostMethodWithNoVideoGameFound_ReturnsRedirectToActionResult()
        {
            // Arrange
            VideoGameResponse? videoGameResponse = null;

            _videoGamesGetterByIdServiceMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGameResponse);

            // Act
            IActionResult result = await _videoGamesController.Delete(videoGameResponse);

            // Assert
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            redirectToActionResult.ActionName.Should().Be("Index");
        }



        // Test should return RedirectToActionResult to Index action method if video game is found and successfully deleted

        [Fact]

        public async Task Delete_ActivateHttpPostMethodSuccessfully_ReturnsRedirectToActionResult()
        {
            // Arrange
            VideoGameResponse videoGameResponse = _fixture
                .Build<VideoGameResponse>()
                .With(x => x.Genre, VideoGameGenre.Action.ToString())
                .Create();

            _videoGamesGetterByIdServiceMock
                .Setup(x => x.GetVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(videoGameResponse);

            _videoGamesDeleterServiceMock
                .Setup(x => x.DeleteVideoGameById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            // Act
            IActionResult result = await _videoGamesController.Delete(videoGameResponse);

            // Assert
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            redirectToActionResult.ActionName.Should().Be("Index");
        }

        #endregion
    }
}
