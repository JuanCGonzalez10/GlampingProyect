using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;

namespace GlampingProyect.Web.Data.Seeders
{
    public class ClientSeeder
    {
        private readonly DataContext _context;

        public ClientSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Client> clients = new List<Client>
            {
                new Client
                {
                    DNI = "9876543210",
                    FirstName = "Lucia",
                    LastName = "Ramírez",
                    Email = "lucia.ramirez@ejemplo.com",
                    Phone = "3109876543",
                    RegisterDate = DateTime.Parse("2025-02-10 08:30:00"),
                    LastPurchaseDate = DateTime.Parse("2025-04-10 12:45:00")
                },
                new Client
                {
                    DNI = "9876543211",
                    FirstName = "David",
                    LastName = "Castaño",
                    Email = "david.castano@ejemplo.com",
                    Phone = "3112223344",
                    RegisterDate = DateTime.Parse("2025-03-01 10:15:30"),
                    LastPurchaseDate = DateTime.Parse("2025-04-12 11:22:10")
                },
                new Client
                {
                    DNI = "9876543212",
                    FirstName = "Isabel",
                    LastName = "Mora",
                    Email = "isabel.mora@ejemplo.com",
                    Phone = "3201112233",
                    RegisterDate = DateTime.Parse("2025-01-22 14:22:15"),
                    LastPurchaseDate = DateTime.Parse("2025-03-28 16:45:00")
                },
                new Client
                {
                    DNI = "9876543213",
                    FirstName = "Sebastián",
                    LastName = "López",
                    Email = "sebastian.lopez@ejemplo.com",
                    Phone = "3004455667",
                    RegisterDate = DateTime.Parse("2025-03-05 09:00:00"),
                    LastPurchaseDate = DateTime.Parse("2025-04-01 13:50:00")
                },
                new Client
                {
                    DNI = "9876543214",
                    FirstName = "Juliana",
                    LastName = "Navarro",
                    Email = "juliana.navarro@ejemplo.com",
                    Phone = "3049988776",
                    RegisterDate = DateTime.Parse("2025-01-30 17:00:00"),
                    LastPurchaseDate = DateTime.Parse("2025-03-22 17:45:00")
                },
                new Client
                {
                    DNI = "9876543215",
                    FirstName = "Felipe",
                    LastName = "Acosta",
                    Email = "felipe.acosta@ejemplo.com",
                    Phone = "3123344556",
                    RegisterDate = DateTime.Parse("2025-02-14 11:12:00"),
                    LastPurchaseDate = DateTime.Parse("2025-03-30 12:35:00")
                },
                new Client
                {
                    DNI = "9876543216",
                    FirstName = "Valentina",
                    LastName = "Ortiz",
                    Email = "valentina.ortiz@ejemplo.com",
                    Phone = "3052233445",
                    RegisterDate = DateTime.Parse("2025-03-12 15:23:00"),
                    LastPurchaseDate = DateTime.Parse("2025-04-05 14:12:00")
                },
                new Client
                {
                    DNI = "9876543217",
                    FirstName = "Tomás",
                    LastName = "Pineda",
                    Email = "tomas.pineda@ejemplo.com",
                    Phone = "3215566778",
                    RegisterDate = DateTime.Parse("2025-02-25 16:05:00"),
                    LastPurchaseDate = DateTime.Parse("2025-04-03 18:50:00")
                },
                new Client
                {
                    DNI = "9876543218",
                    FirstName = "Camila",
                    LastName = "Estrada",
                    Email = "camila.estrada@ejemplo.com",
                    Phone = "3136655443",
                    RegisterDate = DateTime.Parse("2025-03-10 13:35:00"),
                    LastPurchaseDate = DateTime.Parse("2025-04-06 17:00:00")
                },
                new Client
                {
                    DNI = "9876543219",
                    FirstName = "Santiago",
                    LastName = "Ruiz",
                    Email = "santiago.ruiz@ejemplo.com",
                    Phone = "3119988776",
                    RegisterDate = DateTime.Parse("2025-02-18 10:45:00"),
                    LastPurchaseDate = DateTime.Parse("2025-03-27 10:50:00")
                },
                new Client
                {
                    DNI = "9876543220",
                    FirstName = "Mariana",
                    LastName = "Vega",
                    Email = "mariana.vega@ejemplo.com",
                    Phone = "3024455666",
                    RegisterDate = DateTime.Parse("2025-03-17 12:00:00"),
                    LastPurchaseDate = DateTime.Parse("2025-04-07 13:30:00")
                },
                new Client
                {
                    DNI = "9876543221",
                    FirstName = "Nicolás",
                    LastName = "Herrera",
                    Email = "nicolas.herrera@ejemplo.com",
                    Phone = "3141122334",
                    RegisterDate = DateTime.Parse("2025-02-05 09:25:00"),
                    LastPurchaseDate = DateTime.Parse("2025-04-08 14:00:00")
                }
            };

            foreach (Client client in clients)
            {
                bool exists = await _context.Clients.AnyAsync(c => c.Email == client.Email);

                if (!exists)
                {
                    await _context.Clients.AddAsync(client);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
