using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;
using VideoGameLibraryApp.Services.Enums;
using VideoGameLibraryApp.Services.ServiceHelpers.CustomValidators;

namespace VideoGameLibraryApp.Services.DTOs.VideoGameDTOs
{
    public class VideoGameUpdateRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        [MaximumStringLengthAllowedValidator(100, ErrorMessage = "{0} can't be longer than {1} characters!")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        public VideoGameGenre? Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [MaximumStringLengthAllowedValidator(50, ErrorMessage = "{0} can't be longer than {1} characters!")]
        public string? Publisher { get; set; }

        [Display(Name = "Platform Availability")]
        public ICollection<Guid>? VideoGamePlatformIds { get; set; } = new List<Guid>();

        [Display(Name = "Is Multiplayer?")]
        public bool IsMultiplayer { get; set; }

        [Display(Name = "Is Coop?")]
        public bool IsCoop { get; set; }

        public VideoGame ToVideoGame()
        {
            return new VideoGame()
            {
                Id = Id,
                Title = Title ?? string.Empty,
                Genre = Genre.ToString() ?? string.Empty,
                ReleaseDate = ReleaseDate,
                Publisher = Publisher ?? string.Empty,
                IsMultiplayer = IsMultiplayer,
                IsCoop = IsCoop
            };
        }
    }
}
