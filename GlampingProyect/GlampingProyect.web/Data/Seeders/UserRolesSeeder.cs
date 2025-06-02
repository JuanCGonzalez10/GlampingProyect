using DotLiquid.Util;
using  GlampingProyect.Web.Core;
using  GlampingProyect.Web.Data.Entities;
using  GlampingProyect.Web.Data.Entities;
using  GlampingProyect.Web.Services;
using  GlampingProyect.Web.Data;
using Microsoft.EntityFrameworkCore;
using  GlampingProyect.Web.Services;

namespace  GlampingProyect.Web.Data.Seeders
{
    public class UserRolesSeeder
    {
        private readonly DataContext _context;
        private readonly IUsersService _userService;

        public UserRolesSeeder(DataContext context, IUsersService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task SeedAsync()
        {
            await CheckRoles();
            await CheckUsers();
        }

        private async Task CheckUsers()
        {
            // Admin
            User? user = await _userService.GetUserAsync("andreacgonzalezl9@gmail.com");
            if (user is null)
            {
                GlampingRole adminRole = await _context.GlampingRoles.FirstOrDefaultAsync(r => r.Name == Env.SUPER_ADMIN_ROL_NAME);

                user = new User
                {
                    Email = "andreacgonzalezl9@gmail.com",
                    FirstName = "Andrea",
                    LastName = "Gonzalez",
                    PhoneNumber = "1234567890",
                    UserName = "andreacgonzalezl9@gmail.com",
                    Document = "123456",
                    GlampingRole = adminRole
                };

                await _userService.AddUserAsync(user, "1234");

                string token = await _userService.GenerateEmailConfirmationTokenAsync(user);
                await _userService.ConfirmEmailAsync(user, token);
            }
            // contentManager
            user = await _userService.GetUserAsync("juancamilogonzalezh11.1@gmail.com");
            if (user is null)
            {
                GlampingRole contentManagerRole = await _context.GlampingRoles.FirstOrDefaultAsync(r => r.Name == "Gestor de contenido");

                user = new User
                {
                    Email = "juancamilogonzalezh11.1@gmail.com",
                    FirstName = "Camilo",
                    LastName = "Gonzalez",
                    PhoneNumber = "1234567890",
                    UserName = "juancamilogonzalezh11.1@gmail.com",
                    Document = "654321",
                    GlampingRole = contentManagerRole
                };

                await _userService.AddUserAsync(user, "1234");

                string token = await _userService.GenerateEmailConfirmationTokenAsync(user);
                await _userService.ConfirmEmailAsync(user, token);
            }
        }

        private async Task CheckRoles()
        {
            await AdminRolesAsync();
            await ContentManager();
        }

        private async Task ContentManager()
        {
            bool exists = await _context.GlampingRoles.AnyAsync(r => r.Name == "Gestor de contenido");

            if (!exists)
            {
                GlampingRole role = new GlampingRole { Name = "Gestor de contenido" };
                await _context.GlampingRoles.AddAsync(role);

                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Categories" || p.Module == "Glampings")
                                                                         .ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task AdminRolesAsync()
        {
            bool exists = await _context.GlampingRoles.AnyAsync(r => r.Name == Env.SUPER_ADMIN_ROL_NAME);

            if (!exists)
            {
                GlampingRole role = new GlampingRole { Name = Env.SUPER_ADMIN_ROL_NAME };
                await _context.GlampingRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
