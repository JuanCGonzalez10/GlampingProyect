using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;
using GlampingProyect.Web.Data.Entities;

namespace GlampingProyect.Web.Data.Seeders
{
    public class ProductSeeder
    {
        private readonly DataContext _context;

        public ProductSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Product> products = new List<Product>
            {
                new Product
            {
                ProductName = "Glamping pareja",
                ProductDescription = "Alojamiento ecológico en una cabaña de madera con vista a las montañas.",
                ProductPrice = 350.00m,
                ProductBarCode = "9876543210001",
                ProductTax = 0.19,
                IdProductCategory = 1004
            },
            new Product
            {
                ProductName = "Glamping familiar",
                ProductDescription = "Tienda de lujo frente al mar con acceso privado a la playa.",
                ProductPrice = 450.00m,
                ProductBarCode = "9876543210002",
                ProductTax = 0.19,
                IdProductCategory = 1005
            },
            new Product
            {
                ProductName = "Glamping amigos",
                ProductDescription = "Domos geodésicos con calefacción y observación de estrellas en el bosque.",
                ProductPrice = 400.00m,
                ProductBarCode = "9876543210003",
                ProductTax = 0.19,
                IdProductCategory = 1006
            }

            };
            foreach (Product product in products)
            {
                bool exists = await _context.Products.AnyAsync(x => x.ProductBarCode == product.ProductBarCode);
                if (!exists)
                {
                    await _context.Products.AddAsync(product);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
