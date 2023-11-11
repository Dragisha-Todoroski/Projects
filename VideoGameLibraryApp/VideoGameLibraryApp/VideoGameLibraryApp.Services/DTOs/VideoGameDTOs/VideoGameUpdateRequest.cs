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
    public class VideoGameUpdateRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        [StringLength(100, ErrorMessage = "{0} can't be longer than 100 characters!")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        public VideoGameGenre? Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters!")]
        public string? Publisher { get; set; }
        //public ICollection<string>? VideoGamePlatformAvailability { get; set; }

        public ICollection<Guid>? VideoGamePlatformIds { get; set; } = new List<Guid>();
        public bool IsMultiplayer { get; set; }
        public bool IsCoop { get; set; }

        public VideoGame ToVideoGame()
        {
            return new VideoGame()
            {
                Id = Id,
                Title = Title,
                Genre = Genre.ToString(),
                ReleaseDate = ReleaseDate,
                Publisher = Publisher,
                //VideoGamePlatformAvailability = VideoGamePlatformAvailability,
                IsMultiplayer = IsMultiplayer,
                IsCoop = IsCoop
            };
        }
    }
}
