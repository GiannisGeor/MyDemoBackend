using Data;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Services;

namespace Api.Helpers
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContextPool<DemoBackendDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("App"));
            });

            // Services
            services.AddTransient<IStoreService, StoreService>();

            // Repositories
            services.AddTransient<IStoreRepository, StoreRepository>();

            // Services Validators

            return services;
        }

    }
}
