using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlampingProyect.Web.Data.Seeders
{
    public class SaleDetailSeeder
    {
        private readonly DataContext _context;

        public SaleDetailSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<SaleDetail> saleDetails = new List<SaleDetail>
            {
                new SaleDetail
                {
                    SaleDetailCode = "GL001-1",
                    SaleDetailProductQuantity = 1,
                    SaleDetailProductPrice = 320000m,
                    SaleDetailSubtotal = 380800m,
                    SaleDetailProductCode = 1,
                    SaleDetailProductTax = 60800m,
                    IdSale = 1
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL002-1",
                    SaleDetailProductQuantity = 2,
                    SaleDetailProductPrice = 45000m,
                    SaleDetailSubtotal = 107100m,
                    SaleDetailProductCode = 2,
                    SaleDetailProductTax = 17100m,
                    IdSale = 2
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL003-1",
                    SaleDetailProductQuantity = 1,
                    SaleDetailProductPrice = 60000m,
                    SaleDetailSubtotal = 71400m,
                    SaleDetailProductCode = 3,
                    SaleDetailProductTax = 11400m,
                    IdSale = 3
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL004-1",
                    SaleDetailProductQuantity = 2,
                    SaleDetailProductPrice = 320000m,
                    SaleDetailSubtotal = 761600m,
                    SaleDetailProductCode = 1,
                    SaleDetailProductTax = 121600m,
                    IdSale = 4
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL005-1",
                    SaleDetailProductQuantity = 1,
                    SaleDetailProductPrice = 45000m,
                    SaleDetailSubtotal = 53550m,
                    SaleDetailProductCode = 2,
                    SaleDetailProductTax = 8550m,
                    IdSale = 5
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL006-1",
                    SaleDetailProductQuantity = 2,
                    SaleDetailProductPrice = 60000m,
                    SaleDetailSubtotal = 142800m,
                    SaleDetailProductCode = 3,
                    SaleDetailProductTax = 22800m,
                    IdSale = 6
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL007-1",
                    SaleDetailProductQuantity = 1,
                    SaleDetailProductPrice = 320000m,
                    SaleDetailSubtotal = 380800m,
                    SaleDetailProductCode = 1,
                    SaleDetailProductTax = 60800m,
                    IdSale = 7
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL008-1",
                    SaleDetailProductQuantity = 2,
                    SaleDetailProductPrice = 45000m,
                    SaleDetailSubtotal = 107100m,
                    SaleDetailProductCode = 2,
                    SaleDetailProductTax = 17100m,
                    IdSale = 8
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL009-1",
                    SaleDetailProductQuantity = 1,
                    SaleDetailProductPrice = 60000m,
                    SaleDetailSubtotal = 71400m,
                    SaleDetailProductCode = 3,
                    SaleDetailProductTax = 11400m,
                    IdSale = 9
                },
                new SaleDetail
                {
                    SaleDetailCode = "GL010-1",
                    SaleDetailProductQuantity = 1,
                    SaleDetailProductPrice = 320000m,
                    SaleDetailSubtotal = 380800m,
                    SaleDetailProductCode = 1,
                    SaleDetailProductTax = 60800m,
                    IdSale = 10
                }
            };

            foreach (SaleDetail detail in saleDetails)
            {
                bool exists = await _context.SaleDetails.AnyAsync(sd =>
                    sd.SaleDetailCode == detail.SaleDetailCode);

                if (!exists)
                {
                    await _context.SaleDetails.AddAsync(detail);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
