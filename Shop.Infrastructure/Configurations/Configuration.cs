using Microsoft.EntityFrameworkCore;
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
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<ITokenService, TokenService>();
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
            // Configure IdentityCore for API setup
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddEntityFrameworkStores<StoreContext>()
            .AddDefaultTokenProviders();

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
                options.AddPolicy(ClaimStore.Order_Delete, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Order_Delete));


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
                options.AddPolicy(ClaimStore.User_Lockout, policy =>
                    policy.RequireClaim("Permission", ClaimStore.User_Lockout));


                // role management 
                options.AddPolicy(ClaimStore.Role_Create, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Role_Create));
                options.AddPolicy(ClaimStore.Role_Edit, policy =>
                    policy.RequireClaim("Permission", ClaimStore.Role_Edit));

            });
        }
        
    }
}
