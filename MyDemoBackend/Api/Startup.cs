using Api.Helpers;
using AutoMapper;
using Microsoft.OpenApi.Models;

namespace Api
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // Add services
            services.AddSingleton(Configuration);
            services.AddMyServices(Configuration);

            // Automapper configuration
            services.AddAutoMapper(typeof(Startup));

            // Net core default
            services.AddControllers();

            // Swagger
            services.AddSwaggerGen();
            //services.AddSwaggerGen(c =>
            //{
            //    // Support for auth
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //    {
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer",
            //        BearerFormat = "JWT",
            //        In = ParameterLocation.Header,
            //        Description = "Enter {Bearer [space] token in the text input below.\r\n\r\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                }
            //            },
            //            new string[] {}
            //        }
            //    });

            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            // Check automapper config
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            // Development helpers
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Demo API V1");
                });
            }

            // Has to be called before UseRouting!!
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

