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
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IPelatisService, PelatisService>();
            services.AddTransient<IKasetaService, KasetaService>();
            services.AddTransient<IEnoikiasiService, EnoikiasiService>();
            services.AddTransient<ITn_snService, Tn_snService>();

            // Repositories
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IPelatisRepository, PelatisRepository>();
            services.AddTransient<IKasetaRepository, KasetaRepository>();
            services.AddTransient<IEnoikiasiRepository, EnoikiasiRepository>();
            services.AddTransient<ITn_snRepository, Tn_snRepository>();

            // Services Validators
            services.AddScoped<IValidator<ProductDto>, ProductValidator>();

            return services;
        }
    }
}
