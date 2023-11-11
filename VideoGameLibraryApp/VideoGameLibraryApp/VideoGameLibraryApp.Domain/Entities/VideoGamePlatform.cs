using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibraryApp.Domain.Entities
{
    public class VideoGamePlatform
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<VideoGamePlatformAvailability>? VideoGamePlatformAvailability { get; set; } = new List<VideoGamePlatformAvailability>();
    }
}
