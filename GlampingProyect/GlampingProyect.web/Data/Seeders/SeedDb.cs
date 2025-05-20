using GlampingProyect.web.Data.Seeders;
using GlampingProyect.web.Services;

namespace GlampingProyect.Web.Data.Seeders
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserService _usersService;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await new UserRolesSeeder(_context, _usersService).SeedAsync();
            await new PermissionsSeeder(_context).SeedAsync();
            await new CategoriesSeeder(_context).SeedAsync();
            await new GlampingsSeeder(_context).SeedAsync();
        }
    }
}
