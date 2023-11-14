using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibraryApp.Repositories.Abstractions.VideoGamePlatformAvailabilityAbstractions
{
    public interface IVideoGamePlatformAvailabilityDeleterRepository
    {
        Task<bool> DeleteVideoGamePlatformAvailabilityById(Guid videoGamePlatformAvailabilityId);
    }
}
