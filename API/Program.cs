using API.Data;
using API.Extensions;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.SeedDataServices();

var app = builder.Build();


app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
.WithOrigins("https://localhost:4200", "http://localhost:4200"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Map("/", () => Results.Redirect("/swagger"));
app.Run();