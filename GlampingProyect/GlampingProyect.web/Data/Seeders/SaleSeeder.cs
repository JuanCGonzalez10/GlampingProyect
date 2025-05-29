using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;

namespace GlampingProyect.Web.Data.Seeders
{
    public class SaleSeeder
    {
        private readonly DataContext _context;

        public SaleSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Sale> sales = new List<Sale>
            {
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-01-15 10:25:00"),
                    SalePaymentMethod = "Tarjeta",
                    SaleTotal = 4500.00m, // Reserva de cabaña premium 2 noches
                    SaleCode = "G001",
                    IdClient = 1
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-01-28 14:40:00"),
                    SalePaymentMethod = "Efectivo",
                    SaleTotal = 15000.00m, // Reserva familiar, 5 noches + actividades
                    SaleCode = "G002",
                    IdClient = 2
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-02-05 09:10:00"),
                    SalePaymentMethod = "Transferencia",
                    SaleTotal = 2000.00m, // Paquete pareja, 1 noche
                    SaleCode = "G003",
                    IdClient = 1
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-02-12 16:15:00"),
                    SalePaymentMethod = "Cheque",
                    SaleTotal = 2500.00m, // Reserva carpa glamping, 2 noches
                    SaleCode = "G004",
                    IdClient = 3
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-02-27 11:35:00"),
                    SalePaymentMethod = "Tarjeta",
                    SaleTotal = 20000.00m, // Paquete grupo + tour de naturaleza
                    SaleCode = "G005",
                    IdClient = 4
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-03-08 13:00:00"),
                    SalePaymentMethod = "Efectivo",
                    SaleTotal = 1000.00m, // Reserva rápida, 1 noche solo alojamiento
                    SaleCode = "G006",
                    IdClient = 2
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-03-14 15:25:00"),
                    SalePaymentMethod = "Tarjeta",
                    SaleTotal = 5000.00m, // 3 noches, paquete con desayuno y fogata
                    SaleCode = "G007",
                    IdClient = 5
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-03-22 09:55:00"),
                    SalePaymentMethod = "Transferencia",
                    SaleTotal = 15000.00m, // Paquete largo 4 noches, servicios premium
                    SaleCode = "G008",
                    IdClient = 3
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-03-29 17:10:00"),
                    SalePaymentMethod = "Cheque",
                    SaleTotal = 2000.00m, // Reserva estándar, 2 noches
                    SaleCode = "G009",
                    IdClient = 1
                },
                new Sale
                {
                    SaleDate = DateTime.Parse("2025-04-01 12:20:00"),
                    SalePaymentMethod = "Tarjeta",
                    SaleTotal = 24500.00m, // Gran paquete grupo + eventos especiales
                    SaleCode = "G010",
                    IdClient = 4
                }
            };

            foreach (Sale sale in sales)
            {
                bool exists = await _context.Sales.AnyAsync(x => x.SaleCode == sale.SaleCode);

                if (!exists)
                {
                    await _context.Sales.AddAsync(sale);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}