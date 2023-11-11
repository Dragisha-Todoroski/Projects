using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.Domain.Entities;

namespace VideoGameLibraryApp.Services.DTOs.VideoGamePlatformDTOs
{
    public class VideoGamePlatformResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<string>? VideoGamePlatformAvailability { get; set; } = new List<string>();

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(VideoGamePlatformResponse)) return false;

            VideoGamePlatformResponse videoGamePlatformResponse = (VideoGamePlatformResponse)obj;

            return Id == videoGamePlatformResponse.Id && Name == videoGamePlatformResponse.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class VideoGamePlatformExtensions
    {
        public static VideoGamePlatformResponse ToVideoGamePlatformResponse(this VideoGamePlatform videoGamePlatform)
        {
            return new VideoGamePlatformResponse
            {
                Id = videoGamePlatform.Id,
                Name = videoGamePlatform.Name,
                VideoGamePlatformAvailability = videoGamePlatform.VideoGamePlatformAvailability?.Select(x => x.VideoGame?.Title).ToList()
            };
        }
    }
}
