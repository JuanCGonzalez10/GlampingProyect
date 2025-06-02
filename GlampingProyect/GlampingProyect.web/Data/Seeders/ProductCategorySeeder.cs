using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;

namespace GlampingProyect.Web.Data.Seeders
{
    public class ProductCategorySeeder
    {
        private readonly DataContext _context;

        public ProductCategorySeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<ProductCategory> productCategories = new List<ProductCategory>
            {
                  new ProductCategory
                {
                    ProductCategoryName = "Alojamiento de Lujo"
                },
                new ProductCategory
                {
                    ProductCategoryName = "Actividades al Aire Libre"
                },
                new ProductCategory
                {
                    ProductCategoryName = "Servicios de Bienestar"
                }
            };
            foreach (ProductCategory productCategory in productCategories)
            {
                bool exists = await _context.ProductCategories.AnyAsync(x => x.ProductCategoryName == productCategory.ProductCategoryName);
                if (!exists)
                {
                    await _context.ProductCategories.AddAsync(productCategory);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
