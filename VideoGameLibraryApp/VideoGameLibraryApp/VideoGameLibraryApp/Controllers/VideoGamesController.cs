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
        private readonly IVideoGamesGetterByTitleService _videoGamesGetterByTitleService;
        private readonly IVideoGamePlatformsGetterAllService _videoGamePlatformsGetterAllService;

        public VideoGamesController(IVideoGamesGetterAllService videoGamesGetterAllService, IVideoGamesGetterByIdService videoGamesGetterByIdService, IVideoGamesGetterByTitleService videoGamesGetterByTitleService, IVideoGamesAdderService videoGamesAdderService, IVideoGamesUpdaterService videoGamesUpdaterService,  IVideoGamePlatformsGetterAllService videoGamePlatformsGetterAllService, IVideoGamesDeleterService videoGamesDeleterService)
        {
            _videoGamesGetterAllService = videoGamesGetterAllService;
            _videoGamesGetterByIdService = videoGamesGetterByIdService;
            _videoGamesAdderService = videoGamesAdderService;
            _videoGamesUpdaterService = videoGamesUpdaterService;
            _videoGamesGetterByTitleService = videoGamesGetterByTitleService;
            _videoGamePlatformsGetterAllService = videoGamePlatformsGetterAllService;
            _videoGamesDeleterService = videoGamesDeleterService;
        }

        [Route("library")]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var videoGamesList = await _videoGamesGetterAllService.GetAllVideoGames();

            // Grabs display name for Genre if it exists (otherwise grabs value normally)
            foreach (var videoGame in videoGamesList)
            {
                var genre = typeof(VideoGameGenre)
                    .GetMember(videoGame.Genre!.ToString())
                    .FirstOrDefault();

                if (genre == null)
                    videoGame.Genre = videoGame.Genre.ToString();
                else
                {
                    var displayAttribute = genre.GetCustomAttribute<DisplayAttribute>();
                    if (displayAttribute != null)
                    {
                        videoGame.Genre = displayAttribute.GetName();
                    }
                }
            }

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
            VideoGameResponse? videoGameResponse = await _videoGamesGetterByTitleService.GetVideoGameByTitle(videoGameAddRequest.Title);
            if (videoGameResponse != null)
                ModelState.AddModelError("Duplicate Title", "Title already exists!");

            if (!ModelState.IsValid)
            {
                await VideoGameGenreAndPlatformsViewBagSetup();

                return View(videoGameAddRequest);
            }
            
            await _videoGamesAdderService.AddVideoGame(videoGameAddRequest);

            return RedirectToAction("Index");
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
            
            if (videoGameResponseById.Title != videoGameUpdateRequest?.Title)
            {
                VideoGameResponse? videoGameResponse = await _videoGamesGetterByTitleService.GetVideoGameByTitle(videoGameUpdateRequest?.Title);
                if (videoGameResponse != null)
                    ModelState.AddModelError("Duplicate Title", "Title already exists!");
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
