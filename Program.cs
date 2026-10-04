using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using APIdemo.Data;
using APIdemo.Repositories;
using APIdemo.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// Database - SQL Server
// ===============================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// ===============================
// Controllers
// ===============================
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// ===============================
// CORS
// ===============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ===============================
// Repositories
// ===============================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

// ===============================
// Services
// ===============================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();

// ===============================
// Swagger
// ===============================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===============================
// Server-side Blazor
// ===============================
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient("ApiClient", client =>
{
    // Base address will be the same origin
    client.BaseAddress = new Uri(builder.Configuration["ApiBase"] ?? builder.Configuration["ApplicationUrl"] ?? "https://localhost:7258");
});

// Client-side services for Blazor UI
builder.Services.AddScoped<APIdemo.ClientServices.ClientAuthService>();
builder.Services.AddScoped<APIdemo.ClientServices.NotificationService>();

var app = builder.Build();

// ===============================
// HTTP request pipeline
// ===============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthorization();

app.MapControllers();

// Map Blazor hub and fallback to Blazor host page
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();