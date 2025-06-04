using Microsoft.EntityFrameworkCore;
using GlampingProyect.Data;
using GlampingProyect.Web.Data.Entities;

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
                    LastName = "Ramirez",
                    Email = "lucia.ramirez@Correo.com",
                    Phone = "3001234567",
                    RegisterDate = DateTime.Parse("2025-01-01 00:00:00"),
                    LastPurchaseDate = DateTime.Parse("2025-03-23 00:00:00")
                },
                new Client
                {
                    DNI = "9876543211",
                    FirstName = "Sebastian",
                    LastName = "Torres",
                    Email = "sebastian.torres@Correo.com",
                    Phone = "3012345678",
                    RegisterDate = DateTime.Parse("2025-03-24 15:07:56.678"),
                    LastPurchaseDate = DateTime.Parse("2025-03-24 15:07:56.678")
                },
                new Client
                {
                    DNI = "9876543212",
                    FirstName = "Valentina",
                    LastName = "Morales",
                    Email = "valentina.morales@Correo.com",
                    Phone = "3023456789",
                    RegisterDate = DateTime.Parse("2025-03-24 15:37:46.243"),
                    LastPurchaseDate = DateTime.Parse("2025-03-24 15:37:46.243")
                },
                new Client
                {
                    DNI = "9876543213",
                    FirstName = "Andres",
                    LastName = "López",
                    Email = "andres.lopez@Correo.com",
                    Phone = "3034567890",
                    RegisterDate = DateTime.Parse("2025-03-29 19:35:42.375"),
                    LastPurchaseDate = DateTime.Parse("2025-03-29 19:35:42.375")
                },
                new Client
                {
                    DNI = "9876543214",
                    FirstName = "Camila",
                    LastName = "Navarro",
                    Email = "camila.navarro@Correo.com",
                    Phone = "3045678901",
                    RegisterDate = DateTime.Parse("2025-03-29 19:36:58.827"),
                    LastPurchaseDate = DateTime.Parse("2025-03-29 19:36:58.827")
                },
                new Client
                {
                    DNI = "9876543215",
                    FirstName = "Javier",
                    LastName = "Soto",
                    Email = "javier.soto@Correo.com",
                    Phone = "3056789012",
                    RegisterDate = DateTime.Parse("2025-03-29 19:37:33.999"),
                    LastPurchaseDate = DateTime.Parse("2025-03-29 19:37:33.999")
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
