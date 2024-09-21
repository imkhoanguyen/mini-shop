using API.Data;
using API.Errors;
using API.Helpers;
using API.Interfaces;
using API.Repositories;
using API.Services;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                IConfiguration config)
        {
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });
            services.AddSingleton(c =>
                {
                    var cloudinarySettings = config.GetSection("CloudinarySettings").Get<CloudinarySettings>();
                    return new Cloudinary(new Account(
                        cloudinarySettings!.CloudName,
                        cloudinarySettings.ApiKey,
                        cloudinarySettings.ApiSecret
                    ));
                }
            );
            services.AddAuthentication()
                .AddGoogle(option =>
                {
                    IConfigurationSection googleAuthNSection =
                        config.GetSection("Authentication:Google");
                    option.ClientId = googleAuthNSection["ClientId"]!;
                    option.ClientSecret = googleAuthNSection["ClientSecret"]!;
                });



            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IVariantRepository, VariantRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value!.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("CorsPolicy", policy => 
            //    {
            //        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
            //    });
            //});

            return services;
        }
    }
}