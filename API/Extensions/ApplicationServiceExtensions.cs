using API.Configurations;
using API.Data;
using API.Errors;
using API.Helpers;
using API.Interfaces;
using API.Repositories;
using API.Services;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                IConfiguration config)
        {
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
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
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<ICartItemsRepository, CartItemsRepository>();
            services.AddScoped<IShoppingCartRepository,ShoppingCartRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
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

            services.Configure<EmailConfig>(config.GetSection("MailSettings"));
            services.AddScoped<IEmailService, EmailService>();
            // setting thời gian hết hạn của token do asp.net identity tạo ra
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromSeconds(60*5); // 5p
            });

            services.AddSignalR();
            return services;
        }
    }
}