using System.Reflection;
using System.Text;
using Api.Helpers;
using AutoMapper;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Entities.Auth;

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

            // Add authentications
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.SaveToken =
                            false; // Stores tokens created in a list, and only allows tokens found in this list to be valid. If token is valid but not created by this application, then still invalid.
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = bool.Parse(Configuration["Auth:JWT:ValidateIssuer"]),
                            ValidIssuer = Configuration["Auth:JWT:ValidIssuer"],
                            ValidateAudience = bool.Parse(Configuration["Auth:JWT:ValidateAudience"]),
                            ValidAudience = Configuration["Auth:JWT:ValidAudience"],
                            ValidateLifetime = bool.Parse(Configuration["Auth:JWT:ValidateLifetime"]),
                            ClockSkew = TimeSpan.Zero, // We don't want expired tokens to be valid
                            ValidateIssuerSigningKey = bool.Parse(Configuration["Auth:JWT:ValidateIssuerSigningKey"]),
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(Configuration["Auth:JWT:SigningKey"]))
                        };

                        // This method handles the expiration of an token, and sends an appropriate answer to the frontend
                        options.Events = new JwtBearerEvents()
                        {
                            OnAuthenticationFailed = context =>
                            {
                                if (context.Exception != null && context.Exception.Message.Contains("Lifetime"))
                                {
                                    context.Response.StatusCode = 401;
                                    context.Response.Headers.Add("Status", "tokenlifetimeexpired");
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });

            // Add identity
            services.AddDefaultIdentity<AuthUser>(options =>
            {
                options.Lockout = new LockoutOptions()
                {
                    AllowedForNewUsers = bool.Parse(Configuration["Auth:Identity:Lockout:AllowedForNewUsers"]),
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(int.Parse(Configuration["Auth:Identity:Lockout:DefaultLockoutTimeSpan"])),
                    MaxFailedAccessAttempts = int.Parse(Configuration["Auth:Identity:Lockout:MaxFailedAccessAttempts"])
                };

                options.SignIn = new SignInOptions()
                {
                    RequireConfirmedEmail = bool.Parse(Configuration["Auth:Identity:SignIn:RequireConfirmedEmail"]),
                    RequireConfirmedPhoneNumber = bool.Parse(Configuration["Auth:Identity:SignIn:RequireConfirmedPhoneNumber"])
                };

                options.Password = new PasswordOptions()
                {
                    RequireDigit = bool.Parse(Configuration["Auth:Identity:Password:RequireDigit"]),
                    RequireLowercase = bool.Parse(Configuration["Auth:Identity:Password:RequireLowercase"]),
                    RequireNonAlphanumeric = bool.Parse(Configuration["Auth:Identity:Password:RequireNonAlphanumeric"]),
                    RequireUppercase = bool.Parse(Configuration["Auth:Identity:Password:RequireUppercase"]),
                    RequiredLength = int.Parse(Configuration["Auth:Identity:Password:RequiredLength"]),
                    RequiredUniqueChars = int.Parse(Configuration["Auth:Identity:Password:RequiredUniqueChars"])
                };

                options.User = new UserOptions()
                {
                    AllowedUserNameCharacters = Configuration["Auth:Identity:User:AllowedUserNameCharacters"],
                    RequireUniqueEmail = bool.Parse(Configuration["Auth:Identity:User:RequireUniqueEmail"]),

                };

                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DemoBackendDbContext>();

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            // Automapper configuration
            services.AddAutoMapper(typeof(Startup));

            // Net core default
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                                  builder =>
                                  {
                                      builder
                                          .WithOrigins(Configuration.GetSection("AllowedOrigins").Get<string[]>())
                                          .AllowCredentials()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod();
                                  });
            });

            // Swagger
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Template",
                    Description = "This is a template API for the new projects to come!",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Example Contact",
                    //    Url = new Uri("https://example.com/contact")
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Example License",
                    //    Url = new Uri("https://example.com/license")
                    //}
                });

                // Support for auth
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter {Bearer [space] token in the text input below.\r\n\r\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                // Support for comments
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

