using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLibraryApp.DataAccess;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Repositories.Abstractions.VideoGamePlatformAbstractions;
using VideoGameLibraryApp.Repositories.Implementations.VideoGameImplementations;
using VideoGameLibraryApp.Repositories.Implementations.VideoGamePlatformImplementations;
using VideoGameLibraryApp.Services.Abstractions.VideoGameAbstractions;
using VideoGameLibraryApp.Services.Abstractions.VideoGamePlatformAbstractions;
using VideoGameLibraryApp.Services.Implementations.VIdeoGameImplementations;
using VideoGameLibraryApp.Services.Implementations.VIdeoGamePlatformImplementations;

namespace VideoGameLibraryApp.Helpers.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IVideoGamesGetterAllService, VideoGamesGetterAllService>();
            services.AddScoped<IVideoGamesGetterByIdService, VideoGamesGetterByIdService>();
            services.AddScoped<IVideoGamesAdderService, VideoGamesAdderService>();
            services.AddScoped<IVideoGamesUpdaterService, VideoGamesUpdaterService>();
            services.AddScoped<IVideoGamesDeleterService, VideoGamesDeleterService>();

            services.AddScoped<IVideoGamePlatformsGetterAllService, VideoGamePlatformsGetterAllService>();
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IVideoGamesGetterAllRepository, VideoGamesGetterAllRepository>();
            services.AddScoped<IVideoGamesGetterByIdRepository, VideoGamesGetterByIdRepository>();
            services.AddScoped<IVideoGamesAdderRepository, VideoGamesAdderRepository>();
            services.AddScoped<IVideoGamesUpdaterRepository, VideoGamesUpdaterRepository>();
            services.AddScoped<IVideoGamesDeleterRepository, VideoGamesDeleterRepository>();

            services.AddScoped<IVideoGamePlatformsGetterAllRepository, VideoGamePlatformsGetterAllRepository>();
        }
    }
}
