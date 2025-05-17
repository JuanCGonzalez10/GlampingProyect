using AspNetCoreHero.ToastNotification;
using GlampingProyect.web.Data.Entities;
using GlampingProyect.web.Services;
using GlampingProyect.Web.Data;
using GlampingProyect.Web.Data.Seeders;
using GlampingProyect.Web.Helpers;
using GlampingProyect.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;


namespace GlampingProyect.web
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomConfiguration(this WebApplicationBuilder builder)
        {
            // Datacontext 
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // Servicios personalizados
            AddServices(builder);

            // Identity y acceso (¡llámalo aquí!)
            AddIAM(builder);

            // Configuración de notificaciones Toast
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });

            // Configuración de logs
            AddLogConfiguration(builder);

            return builder;
        }

        private static void AddIAM(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, IdentityRole>(conf =>
            {
                conf.User.RequireUniqueEmail = true;

                conf.Password.RequireDigit = false;
                conf.Password.RequiredUniqueChars = 0;
                conf.Password.RequireLowercase = false;
                conf.Password.RequireUppercase = false;
                conf.Password.RequireNonAlphanumeric = false;
                conf.Password.RequiredLength = 4;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(conf =>
            {
                conf.Cookie.Name = "Auth";
                conf.ExpireTimeSpan = TimeSpan.FromDays(100);
                conf.LoginPath = "/Account/Login";
                conf.AccessDeniedPath = "/Errors/403";
            });
        }

        private static void AddLogConfiguration(WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.log",
                              rollingInterval: RollingInterval.Day,
                              restrictedToMinimumLevel: LogEventLevel.Warning)
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            //Servicios
            builder.Services.AddScoped<IGlampingsService, GlampingsService>();
            //builder.Services.AddScoped<IReadLogsService, ReadPlainTexLogstService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddTransient<SeedDb>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Helpers
            builder.Services.AddScoped<ICombosHelper, CombosHelper>();
        }
    }
}
