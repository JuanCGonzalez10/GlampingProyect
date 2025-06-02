using  GlampingProyect.Web.Data.Seeders;
using  GlampingProyect.Web.Services;
using  GlampingProyect.Web.Services;

namespace  GlampingProyect.Web.Data.Seeders
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;

        public SeedDb(DataContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
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
