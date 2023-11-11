using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Services.Enums;

namespace VideoGameLibraryApp.Services.DTOs.VideoGameDTOs
{
    public class VideoGameResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        public string? Publisher { get; set; }

        [Display(Name = "Platform Availability")]
        public ICollection<string>? VideoGamePlatformAvailability { get; set; } = new List<string>();

        [Display(Name = "Is Multiplayer?")]
        public bool IsMultiplayer { get; set; }

        [Display(Name = "Is Co-op?")]
        public bool IsCoop { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(VideoGameResponse)) return false;

            VideoGameResponse videoGameResponse = (VideoGameResponse)obj;

            return Id == videoGameResponse.Id && Title == videoGameResponse.Title && Genre == videoGameResponse.Genre && ReleaseDate == videoGameResponse.ReleaseDate && Publisher == videoGameResponse.Publisher && IsMultiplayer == videoGameResponse.IsMultiplayer && IsCoop == videoGameResponse.IsCoop;
        }

        public VideoGameUpdateRequest ToVideoGameUpdateRequest()
        {
            return new VideoGameUpdateRequest()
            {
                Id = Id,
                Title = Title,
                Genre = (VideoGameGenre)Enum.Parse(typeof(VideoGameGenre), Genre, true),
                ReleaseDate = ReleaseDate,
                Publisher = Publisher,
                //VideoGamePlatformAvailability = (ICollection<VideoGamePlatformAvailability>?)VideoGamePlatformAvailability,
                IsMultiplayer = IsMultiplayer,
                IsCoop = IsCoop
            };
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class VideoGameExtensions
    {
        public static VideoGameResponse ToVideoGameResponse(this VideoGame videoGame)
        {
            return new VideoGameResponse()
            {
                Id = videoGame.Id,
                Title = videoGame.Title,
                Genre = videoGame.Genre,
                ReleaseDate = videoGame.ReleaseDate,
                Publisher = videoGame.Publisher,
                VideoGamePlatformAvailability = videoGame.VideoGamePlatformAvailability?.Select(x => x.VideoGamePlatform?.Name).ToList(),
                IsMultiplayer = videoGame.IsMultiplayer,
                IsCoop = videoGame.IsCoop
            };
        }
    }
}
