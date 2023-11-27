using Services.Dtos;
using Services.Validators;
using FluentValidation;
using Common.Settings;
using Data;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Interfaces.Auth;
using Services.Interfaces.Settings;
using Services.Interfaces.Translations;
using Services.Services;
using Services.Services.Auth;
using Services.Services.Settings;
using Services.Services.Translation;


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
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IApplicationSettingsService, ApplicationSettingsService>();

            services.AddTransient<ITranslationService, TranslationService>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<ISmtpService, SmtpService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IProductService, ProductService>();

            // Repositories
            services.AddTransient<IGenericRepository, GenericRepository>();
            services.AddTransient<ITranslationRepository, TranslationRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IOptionRepository, OptionRepository>();

            // Services Validators
            services.AddScoped<IValidator<NewOrderDto>, NewOrderValidator>();
            services.AddScoped<IValidator<AddressDto>, NewAddressValidator>();
            services.AddScoped<IValidator<AddressDto>, EditAddressValidator>();

            // Settings
            services.Configure<PasswordRequirements>(configuration.GetSection("Auth:Identity:Password"));
            services.Configure<EmailSettingsModel>(configuration.GetSection("Smtp"));


            return services;
        }

    }
}
