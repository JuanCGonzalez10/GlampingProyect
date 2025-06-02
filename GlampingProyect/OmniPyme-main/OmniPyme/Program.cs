using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;
using GlampingProyect.Web;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data.Seeders;
using GlampingProyect.Web.Services; // Asegúrate de importar esto


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//data context
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MyConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    );
});



builder.AddCustomConfiguration();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{ 
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Errors/{0}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.AddCustomWebApplicationConfiguration();

// Ejecutar Seeder
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<Users>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();

    await DataSeeder.SeedAsync(userManager, roleManager);
}
*/

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var seeder = new SeedDb(
        services.GetRequiredService<DataContext>(),
        services.GetRequiredService<IUsersService>()
    );

    await seeder.SeedAsync();
}





app.Run();
