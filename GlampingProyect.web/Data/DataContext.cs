using GlampingProyect.web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PrivateBlog.Web.Data.Entities;
using PrivateBlog.Web.DTOs;


namespace PrivateBlog.Web.Data
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
