using API.Middleware;
using API.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Services.Abstracts;
using Shop.Application.Services.Implementations;
using Shop.Domain.Entities;
using Shop.Infrastructure.Configurations;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.DataAccess.Seed;
using Shop.Infrastructure.Services;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.RegisterDb(builder.Configuration);

builder.Services.AppConfig(builder.Configuration);

builder.Services.AuthConfig(builder.Configuration);

builder.Services.RegisterPolicy(builder.Configuration);

builder.Services.RegisterDI(builder.Configuration);

// register and config redis
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connectionString = builder.Configuration.GetConnectionString("Redis");
    if (connectionString == null)
        throw new Exception("Can not gett redis connection string");
    var configuration  = ConfigurationOptions.Parse(connectionString, true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddSingleton<ICartService, CartService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x
    .WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:5000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseDefaultFiles();

app.MapControllers();



// hub config
app.MapHub<ReviewHub>("hubs/review");
app.MapHub<ChatHub>("/chatHub");


// seed data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<StoreContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await CategorySeed.SeedAsync(context);
    await ProductSeed.SeedAsync(context);
    await RoleSeed.SeedAsync(roleManager);
    await RoleClaimSeed.SeedAsync(context, roleManager);
    await UserSeed.SeedAsync(userManager, roleManager);
    await UserRoleSeed.SeedAsync(userManager, context);
    await ShippingMethodSeed.SeedAsync(context);
    await DiscountSeed.SeedAsync(context);
    await ColorSeed.SeedAsync(context);
    await SizeSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    throw;
}

app.Map("/", () => Results.Redirect("/swagger"));
app.Run();