using  GlampingProyect.Web.Data.Entities;
using  GlampingProyect.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace  GlampingProyect.Web.Data.Seeders
{
    public class PermissionsSeeder
    {
        private readonly DataContext _context;

        public PermissionsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Permission> permissions = [];

            foreach (Permission permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Name == permission.Name && p.Module == permission.Module);

                if (!exists)
                {
                    await _context.Permissions.AddAsync(permission);
                }
            }

            await _context.SaveChangesAsync();
        }

        private List<Permission> Glampings()
        {
            return new List<Permission>
            {
                new Permission { Name = "showGlampings", Description = "Ver Glampings", Module = "Glampings" },
                new Permission { Name = "createGlampings", Description = "Crear Glampings", Module = "Glampings" },
                new Permission { Name = "updateGlampings", Description = "Editar Glampings", Module = "Glampings" },
                new Permission { Name = "deleteGlampings", Description = "Eliminar Glampings", Module = "Glampings" },
            };
        }

        private List<Permission> Categories()
        {
            return new List<Permission>
            {
                new Permission { Name = "showCategoriess", Description = "Ver Categorias", Module = "Categorias" },
                new Permission { Name = "createCategories", Description = "Crear Categorias", Module = "Categorias" },
                new Permission { Name = "updateCategories", Description = "Editar Categorias", Module = "Categorias" },
                new Permission { Name = "deleteCategories", Description = "Eliminar Categorias", Module = "Categorias" },
            };
        }
    }
}
