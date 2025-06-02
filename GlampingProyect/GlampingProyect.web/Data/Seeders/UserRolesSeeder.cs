using GlampingProyect.Web.Core;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data;
using GlampingProyect.Web.Services;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;

namespace GlampingProyect.Web.Data.Seeders
{
    public class UserRolesSeeder
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;

        public UserRolesSeeder(DataContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }

        public async Task SeedAsync()
        {
            await CheckRoles();
            await CheckUsers();
        }

        private async Task CheckUsers()
        {
            // Super Admin
            Users? users = await _usersService.GetUserAsync("admin@glampingproyect.com");

            if (users is null)
            {
                PrivateURole adminRole = await _context.PrivateURoles.FirstOrDefaultAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                users = new Users
                {
                    Email = "admin@glampingproyect.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    PhoneNumber = "+571300000000",
                    UserName = "admin@glampingproyect.com",
                    Document = "900123456",
                    Photo = "https://glampingproyect.com/images/users/admin.jpg",
                    PrivateURole = adminRole
                };

                await _usersService.AddUserAsync(users, "Glamping123!");
                string token = await _usersService.GenerateEmailConfirmationTokenAsync(users);
                await _usersService.ConfirmEmailAsync(users, token);
            }

            // Gerente
            users = await _usersService.GetUserAsync("gerente@glampingproyect.com");

            if (users is null)
            {
                PrivateURole gerenteRole = await _context.PrivateURoles.FirstOrDefaultAsync(r => r.Name == "Gerente");

                users = new Users
                {
                    Email = "gerente@glampingproyect.com",
                    FirstName = "Carlos",
                    LastName = "Montoya",
                    PhoneNumber = "+571310001234",
                    UserName = "gerente@glampingproyect.com",
                    Document = "901234567",
                    Photo = "https://glampingproyect.com/images/users/gerente.jpg",
                    PrivateURole = gerenteRole
                };
                await _usersService.AddUserAsync(users, "Gerente123!");
                string token = await _usersService.GenerateEmailConfirmationTokenAsync(users);
                await _usersService.ConfirmEmailAsync(users, token);
            }

            // Vendedor
            users = await _usersService.GetUserAsync("vendedor@glampingproyect.com");

            if (users is null)
            {
                PrivateURole vendedorRole = await _context.PrivateURoles.FirstOrDefaultAsync(r => r.Name == "Vendedor");

                users = new Users
                {
                    Email = "vendedor@glampingproyect.com",
                    FirstName = "Laura",
                    LastName = "Gómez",
                    PhoneNumber = "+571320002345",
                    UserName = "vendedor@glampingproyect.com",
                    Document = "902345678",
                    Photo = "https://glampingproyect.com/images/users/vendedor.jpg",
                    PrivateURole = vendedorRole
                };
                await _usersService.AddUserAsync(users, "Vendedor123!");
                string token = await _usersService.GenerateEmailConfirmationTokenAsync(users);
                await _usersService.ConfirmEmailAsync(users, token);
            }

            // Gestor de Inventario
            users = await _usersService.GetUserAsync("inventario@glampingproyect.com");

            if (users is null)
            {
                PrivateURole inventarioRole = await _context.PrivateURoles.FirstOrDefaultAsync(r => r.Name == "Gestor de Inventario");

                users = new Users
                {
                    Email = "inventario@glampingproyect.com",
                    FirstName = "Andrés",
                    LastName = "Rodríguez",
                    PhoneNumber = "+571330003456",
                    UserName = "inventario@glampingproyect.com",
                    Document = "903456789",
                    Photo = "https://glampingproyect.com/images/users/inventario.jpg",
                    PrivateURole = inventarioRole
                };
                await _usersService.AddUserAsync(users, "Inventario123!");
                string token = await _usersService.GenerateEmailConfirmationTokenAsync(users);
                await _usersService.ConfirmEmailAsync(users, token);
            }
        }

        private async Task CheckRoles()
        {
            await AdminRolesAsync();
            await ManagerRoleAsync();
            await VendorRoleAsync();
            await InventoryManagerRoleAsync();
        }

        private async Task ManagerRoleAsync()
        {
            bool exists = await _context.PrivateURoles.AnyAsync(r => r.Name == "Gerente");

            if (!exists)
            {
                PrivateURole role = new PrivateURole { Name = "Gerente" };
                await _context.PrivateURoles.AddAsync(role);

                List<Permission> permissions = await _context.Permissions
                    .Where(p => p.Module == "Client" || p.Module == "Product" || p.Module == "ProductCategory" || p.Module == "Sale" || p.Module == "Users")
                    .ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task VendorRoleAsync()
        {
            bool exists = await _context.PrivateURoles.AnyAsync(r => r.Name == "Vendedor");

            if (!exists)
            {
                PrivateURole role = new PrivateURole { Name = "Vendedor" };
                await _context.PrivateURoles.AddAsync(role);

                List<Permission> permissions = await _context.Permissions
                    .Where(p => (p.Module == "Client" || p.Module == "Sale") && !p.Name.StartsWith("Delete"))
                    .ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(new RolePermission { Permission = permission, Role = role });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task InventoryManagerRoleAsync()
        {
            bool exists = await _context.PrivateURoles.AnyAsync(r => r.Name == "Gestor de Inventario");

            if (!exists)
            {
                PrivateURole role = new PrivateURole { Name = "Gestor de Inventario" };
                await _context.PrivateURoles.AddAsync(role);

                List<Permission> permissions = await _context.Permissions
                    .Where(p => (p.Module == "Product" || p.Module == "ProductCategory") && !p.Name.StartsWith("Delete"))
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
            bool exists = await _context.PrivateURoles.AnyAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

            if (!exists)
            {
                PrivateURole role = new PrivateURole { Name = Env.SUPER_ADMIN_ROLE_NAME };
                await _context.PrivateURoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
