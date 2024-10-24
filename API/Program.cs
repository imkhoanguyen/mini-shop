using api.Data.Seed;
using api.SignalR;
using api.SignalR;
using API.Data;
using API.Data.Seed;
using API.Entities;
using API.Data.Seed;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddPolicy();


builder.Services.AddAuthentication();
builder.Services.AddPolicy();





var app = builder.Build();


app.UseCors(x => x
    .WithOrigins("http://localhost:4200", "https://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);
app.UseCors(x => x
    .WithOrigins("http://localhost:4200", "https://localhost:4200")
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
//app.UseCors("CorsPolicy");
//app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseDefaultFiles();
app.MapControllers();

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
app.MapHub<ChatHub>("/chatHub");
app.Map("/", () => Results.Redirect("/swagger"));
app.Run();