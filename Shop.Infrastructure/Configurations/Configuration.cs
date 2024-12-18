﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Services;
using Shop.Application.Ultilities;
using API.Configurations;
using API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shop.Application.Repositories;
using Shop.Infrastructure.Repositories;
using Shop.Application.Services.Implementations;
using Shop.Application.Interfaces;

namespace Shop.Infrastructure.Configurations
{
    public static class Configuration
    {
        public static void RegisterDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");

            // sql server connect
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(connectionString));
        }

        // register dependencies injection
        public static void RegisterDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IVariantService, VariantService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IShippingMethodService, ShippingMethodService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IDiscountService,DiscountService>();
            services.AddScoped<IProductUserLikeService,ProductUserLikeService>();
            services.AddSignalR();
        }

        public static void AppConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.Configure<EmailConfig>(configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailService, EmailService>();
            // setting thời gian hết hạn của token do asp.net identity tạo ra (cái này khác với token do bear tạo ra)
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromSeconds(60 * 5); // 5p
            });
        }

        public static void AuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
            services.AddIdentityApiEndpoints<AppUser>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<StoreContext>();

            // JWT Authentication setup
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]!)),
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidAudience = configuration["Token:Audience"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            //Add Google and Facebook Authentication 
            services.AddAuthentication()
               .AddGoogle(option =>
               {
                   IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");
                   option.ClientId = googleAuthNSection["ClientId"]!;
                   option.ClientSecret = googleAuthNSection["ClientSecret"]!;
               })
               .AddFacebook(option =>
               {
                   IConfigurationSection facebookAuthNSection = configuration.GetSection("Authentication:Facebook");
                   option.AppId = facebookAuthNSection["AppId"]!;
                   option.AppSecret = facebookAuthNSection["AppSecret"]!;
               }
           );
        }

        public static void RegisterPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                //color
                options.AddPolicy(ClaimStore.Color_Create, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Color_Create));
                options.AddPolicy(ClaimStore.Color_Edit, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Color_Edit));
                options.AddPolicy(ClaimStore.Color_Delete, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Color_Delete));


                // category
                options.AddPolicy(ClaimStore.Category_Create, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Category_Create));
                options.AddPolicy(ClaimStore.Category_Edit, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Category_Edit));
                options.AddPolicy(ClaimStore.Category_Delete, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Category_Delete));


                //size
                options.AddPolicy(ClaimStore.Size_Create, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Size_Create));
                options.AddPolicy(ClaimStore.Size_Edit, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Size_Edit));
                options.AddPolicy(ClaimStore.Size_Delete, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Size_Delete));


                // product
                options.AddPolicy(ClaimStore.Product_Create, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Product_Create));
                options.AddPolicy(ClaimStore.Product_Edit, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Product_Edit));
                options.AddPolicy(ClaimStore.Product_Delete, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Product_Delete));


                // other
                options.AddPolicy(ClaimStore.Order_ComfirmPayment, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Order_ComfirmPayment));
                options.AddPolicy(ClaimStore.Order_Comfirm, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Order_Comfirm));
                


                // shipping
                options.AddPolicy(ClaimStore.Shipping_Create, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Shipping_Create));
                options.AddPolicy(ClaimStore.Shipping_Delete, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Shipping_Delete));
                options.AddPolicy(ClaimStore.Shipping_Edit, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Shipping_Edit));


                // user management
                options.AddPolicy(ClaimStore.User_UpdateUserRole, policy =>
                    policy.RequireClaim("Permission", ClaimStore.User_UpdateUserRole));
                options.AddPolicy(ClaimStore.User_Lock, policy =>
                    policy.RequireClaim("Permission", ClaimStore.User_Lock));
                options.AddPolicy(ClaimStore.User_Add, policy =>
                   policy.RequireClaim("Permission", ClaimStore.User_Add));
                options.AddPolicy(ClaimStore.User_Update, policy =>
                   policy.RequireClaim("Permission", ClaimStore.User_Update));


                // role management 
                options.AddPolicy(ClaimStore.Role_Create, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Role_Create));
                options.AddPolicy(ClaimStore.Role_Edit, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Role_Edit));
                options.AddPolicy(ClaimStore.Role_Delete, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Role_Delete));
                options.AddPolicy(ClaimStore.Change_Permission, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Change_Permission));

                //message
                options.AddPolicy(ClaimStore.Message_Reply, policy =>
                   policy.RequireClaim("Permission", ClaimStore.Message_Reply));

                //discount
                options.AddPolicy(ClaimStore.Discount_Create, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Discount_Create));
                options.AddPolicy(ClaimStore.Discount_Delete, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Discount_Delete));
                options.AddPolicy(ClaimStore.Discount_Edit, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Discount_Edit));

                //access admin page
                options.AddPolicy(ClaimStore.Access_Admin, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Access_Admin));
            });
        }

    }
}
