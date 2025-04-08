using GlampingProyect.web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.DTOs;


namespace GlampingProyect.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Section> Sections { get; set; }
    }
}
