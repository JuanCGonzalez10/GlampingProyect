using GlampingProyect.web.Data.Entities;       // User
using GlampingProyect.Web.Data;                // DataContext
using GlampingProyect.Web.Services;            // IUserService, UserService
using GlampingProyect.Web.Helpers;             // AddCustomConfiguration
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Serilog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;  // IEmailSender
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Services;
using GlampingProyect.web;
using GlampingProyect.web.Services;            // SmtpEmailSender

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

// 3. Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/NoAuthorized";
});

// 4. MVC
builder.Services.AddControllersWithViews();

// 5. Toast notifications
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

// 6. AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 7. Custom services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IGlampingsService, GlampingsService>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();

// 8. Email sender for Identity UI
IServiceCollection serviceCollection = builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

// 9. Any other custom configuration
builder.AddCustomConfiguration();

// 10. Serilog
builder.Host.UseSerilog();

var app = builder.Build();

// --- Middleware pipeline ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Errors/{0}");

app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
