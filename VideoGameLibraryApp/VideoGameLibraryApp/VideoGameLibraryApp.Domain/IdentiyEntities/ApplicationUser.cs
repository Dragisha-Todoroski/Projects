using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;

namespace VideoGameLibraryApp.Domain.IdentiyEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<VideoGame>? VideoGames { get; set; } = new List<VideoGame>();
    }
}
