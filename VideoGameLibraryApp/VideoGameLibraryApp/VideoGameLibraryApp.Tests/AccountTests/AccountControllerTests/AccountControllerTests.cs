using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using VideoGameLibraryApp.Controllers;
using VideoGameLibraryApp.Domain.IdentiyEntities;
using VideoGameLibraryApp.Services.DTOs.AccountDTOs;
using VideoGameLibraryApp.Services.Enums;

namespace VideoGameLibraryApp.Tests.AccountTests.AccountControllerTests
{
    public class AccountControllerTests
    {
        private readonly AccountController _accountController;

        private readonly Mock<UserManager<ApplicationUser?>> _userManagerMock;
        private readonly UserManager<ApplicationUser?> _userManager;

        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly Mock<RoleManager<ApplicationRole>> _roleManagerMock;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IFixture _fixture;

        public AccountControllerTests()
        {
            _userManagerMock = new Mock<UserManager<ApplicationUser?>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            _userManager = _userManagerMock.Object;

            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManager,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<ApplicationUser>>().Object);

            _signInManager = _signInManagerMock.Object;

            _roleManagerMock = new Mock<RoleManager<ApplicationRole>>(
                new Mock<IRoleStore<ApplicationRole>>().Object,
                new IRoleValidator<ApplicationRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<ApplicationRole>>>().Object);

            _roleManager = _roleManagerMock.Object;

            _accountController = new AccountController(_userManager, _signInManager, _roleManager);

            _fixture = new Fixture();
        }


        #region Register

        // Test should return ViewResult with appropriate Model

        [Fact]

        public void Register_ActivateHttpGetMethodSuccessfully_ReturnsViewResult()
        {
            // Act
            IActionResult result = _accountController.Register();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
        }



        // Test should return ViewResult with appropriate Model in the case of model errors

        [Fact]

        public async Task Register_IfModelErrorsForValidationInHttpPostMethod_ReturnsViewResultWithCorrectModel()
        {
            // Assert
            RegisterDTO registerDTO = _fixture.Create<RegisterDTO>();

            // Act
            _accountController.ModelState.AddModelError(nameof(registerDTO.Email), "Email is not in the correct format!");

            IActionResult result = await _accountController.Register(registerDTO);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<RegisterDTO>();

            viewResult.Model.Should().BeEquivalentTo(registerDTO);
        }



        // Test should return RedirectToActionResult to Index action method in VideoGamesController in the case of no model errors after adding user

        [Fact]

        public async Task Register_IfNoModelErrorsInHttpPostMethod_ReturnsRedirectToActionResultToVideoGamesControllerIndex()
        {
            // Arrange
            RegisterDTO registerDTO = _fixture.Create<RegisterDTO>();

            ApplicationRole applicationRole = _fixture.Create<ApplicationRole>();

            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock
                .Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _signInManagerMock
                .Setup(x => x.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), null))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            _roleManagerMock
                .Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(applicationRole));

            _roleManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationRole>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            IActionResult result = await _accountController.Register(registerDTO);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            redirectResult.ControllerName.Should().Be("VideoGames");
            redirectResult.ActionName.Should().Be("Index");
        }



        // Test should return ViewResult with appropriate Model in the case of model errors due to failed user creation and adding

        [Fact]

        public async Task Register_IfModelErrorsForIdentityResultFailureInHttpPostMethod_ReturnsViewResultWithCorrectModel()
        {
            // Arrange
            RegisterDTO registerDTO = _fixture.Create<RegisterDTO>();

            _userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError
                {
                    Description = "Error"
                }));

            // Act
            IActionResult result = await _accountController.Register(registerDTO);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<RegisterDTO>();

            viewResult.Model.Should().BeEquivalentTo(registerDTO);
        }

        #endregion

        #region Login

        // Test should return ViewResult with appropriate Model

        [Fact]

        public void Login_ActivateHttpGetMethodSuccessfully_ReturnsViewResult()
        {
            // Act
            IActionResult result = _accountController.Login();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
        }



        // Test should return ViewResult with appropriate Model in the case of model errors

        [Fact]

        public async Task Login_IfModelErrorsForValidationInHttpPostMethod_ReturnsViewResultWithCorrectModel()
        {
            // Assert
            LoginDTO loginDTO = _fixture.Create<LoginDTO>();

            // Act
            _accountController.ModelState.AddModelError(nameof(loginDTO.Email), "Email is not in the correct format!");

            IActionResult result = await _accountController.Login(loginDTO, null);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<LoginDTO>();

            viewResult.Model.Should().BeEquivalentTo(loginDTO);
        }


        // Test should return ViewResult with appropriate Model if user is not found by email

        [Fact]

        public async Task Login_IfUserNotFoundByEmail_ReturnsViewResultWithCorrectModel()
        {
            // Arrange
            LoginDTO loginDTO = _fixture.Create<LoginDTO>();

            ApplicationUser? user = null;

            _userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            IActionResult result = await _accountController.Login(loginDTO, null);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<LoginDTO>();

            viewResult.Model.Should().BeEquivalentTo(loginDTO);
        }



        // Test should return LocalRedirectResult to the return url after logging in

        [Fact]
        public async Task Login_IfNoModelErrorsAndRedirectUrlApplicable_ReturnsLocalRedirectResultToRedirectUrl()
        {
            // Arrange
            LoginDTO loginDTO = _fixture.Create<LoginDTO>();

            ApplicationUser user = _fixture
                .Build<ApplicationUser>()
                .Without(x => x.VideoGames)
                .Create();

            string returnUrl = "/Controller/Action";

            HttpContext httpContext = new DefaultHttpContext();

            Mock<UrlHelper> urlHelper = new Mock<UrlHelper>(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()));

            urlHelper
                .Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(true);

            _accountController.Url = urlHelper.Object;

            _userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _signInManagerMock
                .Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            IActionResult result = await _accountController.Login(loginDTO, returnUrl);

            // Assert
            LocalRedirectResult localRedirectResult = Assert.IsType<LocalRedirectResult>(result);

            localRedirectResult.Url.Should().Be(returnUrl);
        }



        // Test should return RedirectToActionResult to Index action method in VideoGamesController in the case of no model errors after logging in

        [Fact]

        public async Task Login_IfNoModelErrorsInHttpPostMethod_ReturnsRedirectToActionResultToVideoGamesControllerIndex()
        {
            // Arrange
            LoginDTO loginDTO = _fixture.Create<LoginDTO>();

            ApplicationUser user = _fixture
                .Build<ApplicationUser>()
                .Without(x => x.VideoGames)
                .Create();

            _userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _signInManagerMock
                .Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            IActionResult result = await _accountController.Login(loginDTO, null);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            redirectResult.ControllerName.Should().Be("VideoGames");
            redirectResult.ActionName.Should().Be("Index");
        }



        // Test should return ViewResult with appropriate Model in the case of model errors due to failed user creation and adding

        [Fact]

        public async Task Login_IfModelErrorsForSignInResultFailureInHttpPostMethod_ReturnsViewResultWithCorrectModel()
        {
            // Arrange
            LoginDTO loginDTO = _fixture.Create<LoginDTO>();

            ApplicationUser user = _fixture
                .Build<ApplicationUser>()
                .Without(x => x.VideoGames)
                .Create();

            _userManagerMock
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _signInManagerMock
                .Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            IActionResult result = await _accountController.Login(loginDTO, null);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.Model.Should().BeOfType<LoginDTO>();

            viewResult.Model.Should().BeEquivalentTo(loginDTO);
        }

        #endregion

        #region Logout

        // Test should return RedirectToAction to Login action method

        [Fact]

        public async Task Logout_IfSignedOutCorrectly_ReturnsRedirectToActionResultToLogin()
        {
            // Arrange
            _signInManagerMock
                .Setup(x => x.SignOutAsync())
                .Returns(Task.CompletedTask);

            // Act
            IActionResult result = await _accountController.Logout();

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            redirectResult.ActionName.Should().Be("Login");
        }

        #endregion
    }
}