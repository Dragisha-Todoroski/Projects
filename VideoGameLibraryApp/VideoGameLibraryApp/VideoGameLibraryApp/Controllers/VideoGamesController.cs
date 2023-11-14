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
        private readonly IVideoGamesGetterByIdService _videoGamesGetterByIdService;
        private readonly IVideoGamesAdderService _videoGamesAdderService;
        private readonly IVideoGamesUpdaterService _videoGamesUpdaterService;
        private readonly IVideoGamesDeleterService _videoGamesDeleterService;
        private readonly IVideoGamesDuplicateCheckerService _videoGamesDuplicateChecker;
        private readonly IVideoGamePlatformsGetterAllService _videoGamePlatformsGetterAllService;

        public VideoGamesController(IVideoGamesGetterAllService videoGamesGetterAllService, IVideoGamesGetterByIdService videoGamesGetterByIdService, IVideoGamesDuplicateCheckerService videoGamesDuplicateChecker, IVideoGamesAdderService videoGamesAdderService, IVideoGamesUpdaterService videoGamesUpdaterService,  IVideoGamePlatformsGetterAllService videoGamePlatformsGetterAllService, IVideoGamesDeleterService videoGamesDeleterService)
        {
            _videoGamesGetterAllService = videoGamesGetterAllService;
            _videoGamesGetterByIdService = videoGamesGetterByIdService;
            _videoGamesAdderService = videoGamesAdderService;
            _videoGamesUpdaterService = videoGamesUpdaterService;
            _videoGamesDuplicateChecker = videoGamesDuplicateChecker;
            _videoGamePlatformsGetterAllService = videoGamePlatformsGetterAllService;
            _videoGamesDeleterService = videoGamesDeleterService;
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
            await VideoGameGenreAndPlatformsViewBagSetup();

            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(VideoGameAddRequest videoGameAddRequest)
        {
            try
            {
                await _videoGamesDuplicateChecker.CheckForDuplicateTitle(videoGameAddRequest.Title);
                if (!ModelState.IsValid)
                {
                    await VideoGameGenreAndPlatformsViewBagSetup();

                    return View(videoGameAddRequest);
                }

                VideoGameResponse videoGameResponse = await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);

                return RedirectToAction("Index");
            }
            catch (DuplicateVideoGameTitleException ex)
            {
                ModelState.AddModelError("Title", ex.Message);

                await VideoGameGenreAndPlatformsViewBagSetup();

                return View(videoGameAddRequest);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Edit(Guid? videoGameId)
        {
            VideoGameResponse? videoGameResponseById = await _videoGamesGetterByIdService.GetVideoGameById(videoGameId);
            if (videoGameResponseById == null)
            {
                return RedirectToAction("Index");
            }

            VideoGameUpdateRequest videoGameUpdateRequest = videoGameResponseById.ToVideoGameUpdateRequest();

            await VideoGameGenreAndPlatformsViewBagSetup(videoGameUpdateRequest?.Genre.ToString());

            return View(videoGameUpdateRequest);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit(VideoGameUpdateRequest? videoGameUpdateRequest)
        {
            VideoGameResponse? videoGameResponseById = await _videoGamesGetterByIdService.GetVideoGameById(videoGameUpdateRequest?.Id);
            if (videoGameResponseById == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                await VideoGameGenreAndPlatformsViewBagSetup(videoGameUpdateRequest?.Genre.ToString());

                return View(videoGameUpdateRequest);
            }

            await _videoGamesUpdaterService.UpdateVideoGame(videoGameUpdateRequest);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{videoGameId}")]
        public async Task<IActionResult> Delete(Guid? videoGameId)
        {
            VideoGameResponse? videoGameResponseById = await _videoGamesGetterByIdService.GetVideoGameById(videoGameId);
            if (videoGameResponseById == null)
                return RedirectToAction("Index");

            return View(videoGameResponseById);
        }

        [HttpPost]
        [Route("[action]/{videoGameId}")]
        public async Task<IActionResult> Delete(VideoGameResponse? videoGameResponse)
        {
            VideoGameResponse? videoGameResponseById = await _videoGamesGetterByIdService.GetVideoGameById(videoGameResponse?.Id);
            if (videoGameResponseById == null)
                return RedirectToAction("Index");

            await _videoGamesDeleterService.DeleteVideoGameById(videoGameResponseById.Id);

            return RedirectToAction("Index");
        }

        private async Task VideoGameGenreAndPlatformsViewBagSetup(string? selectedGenre = null)
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
                    Value = x.ToString(),
                    Selected = x.ToString() == selectedGenre
                })
                .ToList();

            ViewBag.Genres.Insert(0, new SelectListItem
            {
                Text = "Select genre...",
                Disabled = true,
                Selected = string.IsNullOrEmpty(selectedGenre),
                Value = "-1"
            });

            ViewBag.VideoGamePlatforms = await _videoGamePlatformsGetterAllService.GetAllVideoGamePlatforms();
        }
    }
}
