using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;

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
                    ProductName = "Tienda Safari Premium",
                    ProductDescription = "Alojamiento tipo safari con cama king, baño privado y vista panorámica.",
                    ProductPrice = 320.00m,
                    ProductBarCode = "7894561230001",
                    ProductTax = 0.19,
                    IdProductCategory = 1, // Alojamiento de Lujo
                },
                new Product
                {
                    ProductName = "Tour en Bicicleta de Montaña",
                    ProductDescription = "Recorrido guiado por senderos naturales durante 2 horas.",
                    ProductPrice = 45.00m,
                    ProductBarCode = "7894561230002",
                    ProductTax = 0.19,
                    IdProductCategory = 2, // Actividades al Aire Libre
                },
                new Product
                {
                    ProductName = "Masaje Relajante en Cabaña",
                    ProductDescription = "Masaje de cuerpo completo de 60 minutos en ambiente natural.",
                    ProductPrice = 60.00m,
                    ProductBarCode = "7894561230003",
                    ProductTax = 0.19,
                    IdProductCategory = 3, // Servicios de Bienestar
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
