using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.IdentiyEntities;

namespace VideoGameLibraryApp.Domain.Entities
{
    public class VideoGame
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, ErrorMessage = "{0} can't be longer than 100 characters!")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters!")]
        public string Genre { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, ErrorMessage = "{0} can't be longer than 50 characters!")]
        public string? Publisher { get; set; }
        public virtual ICollection<VideoGamePlatformAvailability>? VideoGamePlatformAvailability { get; set; } = new List<VideoGamePlatformAvailability>();
        public bool IsMultiplayer { get; set; }
        public bool IsCoop { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? User { get; set; }
    }
}
