using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibraryApp.Domain.Entities
{
    public class VideoGamePlatformAvailability
    {
        [Key]
        public Guid Id { get; set; }
        public Guid VideoGameId { get; set; }
        public Guid VideoGamePlatformId { get; set; }

        [ForeignKey(nameof(VideoGameId))]
        public virtual VideoGame? VideoGame { get; set; }

        [ForeignKey(nameof(VideoGamePlatformId))]
        public virtual VideoGamePlatform? VideoGamePlatform { get; set; }
    }
}
