using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data.Entities;

namespace GlampingProyect.Web.Data.Seeders
{
    public class GlampingsSeeder
    {
        private readonly DataContext _context;

        public GlampingsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            Category category = await _context.Categories.FirstOrDefaultAsync();

            if (category == null)
            {
                return;
            }

            List<Glamping> glampings = new List<Glamping>
            {
                new Glamping { Name = "Glamping 1", CategoryId = category.Id, Content = "<p>Glamping 1</p>", IsPublished = true },
                new Glamping { Name = "Glamping 2", CategoryId = category.Id, Content = "<p>Glamping 2</p>", IsPublished = true },
                new Glamping { Name = "Glamping 3", CategoryId = category.Id, Content = "<p>Glamping 3</p>", IsPublished = true }
            };

            foreach (Glamping glamping in glampings)
            {
                bool exists = await _context.Glampings.AnyAsync(g => g.Name == glamping.Name && g.CategoryId == glamping.CategoryId);

                if (!exists)
                {
                    await _context.Glampings.AddAsync(glamping);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
