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
                new Category { Name = "General", Description = "Información básica de la compañía" },
                new Category { Name = "Telecomunicaciones" },
                new Category { Name = "Hacking" },
                new Category { Name = "Clases", IsHidden = true },
                new Category { Name = "Informática" },
                new Category { Name = "Pentesting" },
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
