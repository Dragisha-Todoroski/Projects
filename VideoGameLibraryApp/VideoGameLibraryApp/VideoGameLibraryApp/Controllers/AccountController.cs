using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using VideoGameLibraryApp.Domain.IdentiyEntities;
using VideoGameLibraryApp.Services.DTOs.AccountDTOs;
using VideoGameLibraryApp.Services.Enums;

namespace VideoGameLibraryApp.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser?> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser?> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize("NotAuthenticated")]
        public IActionResult Register()
        {
            ViewBag.UserRoles = Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Select(x => new SelectListItem
                {
                    Text = x.GetType()
                        .GetMember(x.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?
                        .GetName() ?? x.ToString(),
                    Value = x.ToString()
                })
                .ToList();

            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize("NotAuthenticated")]
        [ValidateAntiForgeryToken]
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
                if (registerDTO.UserRole == UserRole.Admin)
                {
                    // Check if 'Admin' role has been created (once created, IF block shouldn't trigger)
                    await FindAndCreateNewRole(UserRole.Admin);

                    // Add new user to 'Admin' role
                    await _userManager.AddToRoleAsync(applicationUser, UserRole.Admin.ToString());
                }
                else
                {
                    // Check if 'User' role has been created (once created, IF block shouldn't trigger)
                    await FindAndCreateNewRole(UserRole.User);

                    // Add new user to 'User' role
                    await _userManager.AddToRoleAsync(applicationUser, UserRole.User.ToString());
                }

                await _signInManager.SignInAsync(applicationUser, isPersistent: true);

                return RedirectToAction(nameof(VideoGamesController.Index), "VideoGames");
            }

            foreach (var error in identityResult.Errors)
                ModelState.AddModelError("Register", error.Description);

            return View(registerDTO);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize("NotAuthenticated")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize("NotAuthenticated")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(loginDTO);

            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return View(loginDTO);

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return LocalRedirect(returnUrl);

                return RedirectToAction(nameof(VideoGamesController.Index), "VideoGames");
            }

            ModelState.AddModelError("Login", "Invalid Email or Password");

            return View(loginDTO);
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public async Task FindAndCreateNewRole(UserRole userRole)
        {
            if (await _roleManager.FindByNameAsync(userRole.ToString()) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = userRole.ToString()
                };

                await _roleManager.CreateAsync(applicationRole);
            }
        }
    }
}
