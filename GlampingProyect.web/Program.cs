using GlampingProyect.web;
using GlampingProyect.Web.Services;
using GlampingProyect.Web.Helpers;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Notyf;
using AutoMapper;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// ✅ Notificaciones (AspNetCoreHero.ToastNotification)
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

// ✅ AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// ✅ Servicios personalizados
builder.Services.AddScoped<ISectionsService, SectionsService>();
builder.Services.AddScoped<IBlogsService, BlogsService>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();

// ✅ Configuración personalizada (base de datos, etc.)
builder.AddCustomConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Middleware para notificaciones toast
app.UseNotyf();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
