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


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.RegisterDb(builder.Configuration);

builder.Services.AppConfig(builder.Configuration);

builder.Services.AuthConfig(builder.Configuration);

builder.Services.RegisterPolicy(builder.Configuration);

builder.Services.RegisterDI(builder.Configuration);


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
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    throw;
}

app.Map("/", () => Results.Redirect("/swagger"));
app.Run();