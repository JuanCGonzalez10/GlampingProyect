using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Data.Entities;

namespace GlampingProyect.Web.Data.Seeders
{
    public class CategoriesSeeder
    {
        private readonly DataContext _context;

        public CategoriesSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Category> categories = new List<Category>
    {
            new Category { Name = "Alojamiento", Description = "Tipos de alojamiento disponibles: cabañas, domos, casas en árboles" },
            new Category { Name = "Gastronomía", Description = "Menús disponibles, cocina local, opciones vegetarianas" },
            new Category { Name = "Bienestar", Description = "Spa, yoga, masajes y experiencias relajantes" },
            new Category { Name = "Reservas", Description = "Información relacionada con reservas y disponibilidad" },
            new Category { Name = "Normas del lugar", Description = "Reglas de comportamiento y convivencia" },
            new Category { Name = "Servicios adicionales", Description = "Wi-Fi, transporte, estacionamiento, etc." }
    };

            foreach (Category category in categories)
            {
                bool exists = await _context.Categories.AnyAsync(c => c.Name == category.Name);

                if (!exists)
                {
                    await _context.Categories.AddAsync(category);
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}
