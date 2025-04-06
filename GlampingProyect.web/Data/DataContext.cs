
using GlampingProyect.web.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace GlampingProyect.web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
           {
           }

        public DbSet<Section> Sections { get; set; }
    }
}
