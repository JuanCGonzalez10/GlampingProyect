using GlampingProyect.Web.Data;                 // DataContext
using GlampingProyect.Web.Data.Entities;        // User
using GlampingProyect.Web.Helpers;              // AddCustomConfiguration extension
using GlampingProyect.Web.Services;             // IUserService, etc.
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services; // IEmailSender
using Microsoft.EntityFrameworkCore;
using Serilog;
using GlampingProyect.web.Data.Entities;
using GlampingProyect.web.Services;
using GlampingProyect.web;

var builder = WebApplication.CreateBuilder(args);

// 1) DbContext ------------------------------------------------------------
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Identity -------------------------------------------------------------
// Si solo necesitas autenticación por cookies de Identity, 
// AddDefaultIdentity es suficiente y registra el esquema una sola vez.
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddRoles<IdentityRole>()                   // ← mantén roles si los usas
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

// 3) Cookie path settings --------------------------------------------------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/NoAuthorized";
});

// 4) MVC & Razor views -----------------------------------------------------
builder.Services.AddControllersWithViews();

// 5) Toast notifications ---------------------------------------------------
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

// 6) AutoMapper ------------------------------------------------------------
builder.Services.AddAutoMapper(typeof(Program));

// 7) Domain services -------------------------------------------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IGlampingsService, GlampingsService>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();

// 8) Email sender ----------------------------------------------------------
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

// 9) Otras extensiones personalizadas -------------------------------------
builder.AddCustomConfiguration();

// 10) Serilog --------------------------------------------------------------
builder.Host.UseSerilog();

var app = builder.Build();

// ------------------- Middleware pipeline ----------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // solo una vez
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Errors/{0}");
app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
