using Data;
using Data.Interfaces;
using Data.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;
using Services.Validators;


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
            services.AddTransient<IOrderService, OrderService>();

            // Repositories
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            // Services Validators
            services.AddScoped<IValidator<NewOrderDto>, NewOrderValidator>();

            return services;
        }

    }
}
