using GlampingProyect.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Data.Entities;

namespace GlampingProyect.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Glamping> Glampings { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
