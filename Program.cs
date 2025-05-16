using Microsoft.EntityFrameworkCore;
using MovieProject.Models;
using MovieProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Enable session management
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Add app-specific services
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<UserService>();

// 👇 This allows _Layout.cshtml and other views to access session via DI
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure middleware
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // 👈 Make sure this is before authorization if added
app.UseAuthorization();

// Map default controller route
app.MapDefaultControllerRoute();

app.Run();
