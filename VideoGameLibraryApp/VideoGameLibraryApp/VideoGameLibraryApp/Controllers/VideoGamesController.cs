using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using VideoGameLibraryApp.Exceptions.CustomExceptions;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGamePlatformAbstractions;
using VideoGameLibraryApp.Services.DTOs.VideoGameDTOs;
using VideoGameLibraryApp.Services.Enums;

namespace VideoGameLibraryApp.Controllers
{
    [Route("[controller]")]
    public class VideoGamesController : Controller
    {
        private readonly IVideoGamesGetterAllService _videoGamesGetterAllService;
        private readonly IVideoGamesAdderService _videoGamesAdderService;
        private readonly IVideoGamePlatformsGetterAllService _videoGamePlatformsGetterAllService;
        //private readonly IVideoGamePlatformsGetterAllService _videoGamePlatformsGetterAllService;

        public VideoGamesController(IVideoGamesGetterAllService videoGamesGetterAllService, IVideoGamesAdderService videoGamesAdderService, IVideoGamePlatformsGetterAllService videoGamePlatformsGetterAllService)
        {
            _videoGamesGetterAllService = videoGamesGetterAllService;
            _videoGamesAdderService = videoGamesAdderService;
            _videoGamePlatformsGetterAllService = videoGamePlatformsGetterAllService;
        }

        [Route("[controller]/library")]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var videoGamesList = await _videoGamesGetterAllService.GetAllVideoGames();

            return View(videoGamesList);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Create()
        {
            await VideoGameGenreViewBagSetup();

            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(VideoGameAddRequest videoGameAddRequest)
        {
            try
            {
                await _videoGamesAdderService.CheckForDuplicateTitle(videoGameAddRequest.Title);
                if (!ModelState.IsValid)
                {
                    await VideoGameGenreViewBagSetup();

                    return View(videoGameAddRequest);
                }

                //await _videoGamesAdderService.CheckForDuplicateTitle(videoGameAddRequest.Title);

                VideoGameResponse videoGameResponse = await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);

                return RedirectToAction("Index");
            }
            catch (DuplicateVideoGameTitleException ex)
            {
                ModelState.AddModelError("Title", ex.Message);

                await VideoGameGenreViewBagSetup();

                return View(videoGameAddRequest);
            }
        }

        private async Task VideoGameGenreViewBagSetup()
        {
            ViewBag.Genres = Enum.GetValues(typeof(VideoGameGenre))
                .Cast<VideoGameGenre>()
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

            ViewBag.Genres.Insert(0, new SelectListItem
            {
                Text = "Select genre!",
                Disabled = true,
                Selected = true,
                Value = "-1"
            });

            ViewBag.VideoGamePlatforms = await _videoGamePlatformsGetterAllService.GetAllVideoGamePlatforms();
        }
    }
}
