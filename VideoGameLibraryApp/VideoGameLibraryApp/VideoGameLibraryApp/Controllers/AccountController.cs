using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibraryApp.Domain.IdentiyEntities;
using VideoGameLibraryApp.Services.DTOs.AccountDTOs;

namespace VideoGameLibraryApp.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return View(registerDTO);

            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };
            
            IdentityResult identityResult = await _userManager.CreateAsync(applicationUser, registerDTO.Password);
            if (identityResult.Succeeded)
            {
                await _signInManager.SignInAsync(applicationUser, isPersistent: true);

                return RedirectToAction(nameof(VideoGamesController.Index), "VideoGames");
            }

            foreach (var error in identityResult.Errors)
                ModelState.AddModelError("Register", error.Description);

            return View(registerDTO);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return View(loginDTO);

            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return View(loginDTO);

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
                return RedirectToAction(nameof(VideoGamesController.Index), "VideoGames");

            ModelState.AddModelError("Login", "Invalid Email or Password");

            return View(loginDTO);
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
