using GlampingProyect.web.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GlampingProyect.web
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder addCustomConfiguration(this WebApplicationBuilder builder)
        {
            //Datacontext 
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });
            return builder;
        }
    }
}
