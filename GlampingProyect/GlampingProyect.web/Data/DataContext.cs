using GlampingProyect.web.Data.Entities; // Tu clase User y entidades personalizadas
using GlampingProyect.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlampingProyect.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // DbSets personalizados
        public DbSet<Glamping> Glampings { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<GlampingRole> GlampingRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RoleCategory> RoleCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<Section> Sections { get; set; } // ← Añadido para RoleSection

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // ← ¡Muy importante! Identity se configura primero

            ConfigureIndexes(builder);
            ConfigureKeys(builder);
        }

        private void ConfigureIndexes(ModelBuilder builder)
        {
            // Roles
            builder.Entity<GlampingRole>().HasIndex(r => r.Name).IsUnique();

            // Sections
            builder.Entity<Category>().HasIndex(s => s.Name).IsUnique();

            // Users
            builder.Entity<User>().HasIndex(u => u.Document).IsUnique();
        }

        private void ConfigureKeys(ModelBuilder builder)
        {
            // Role-Permission (many-to-many)
            builder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // Role-Section (many-to-many)
            builder.Entity<RoleCategory>().HasKey(rs => new { rs.RoleId, rs.CategoryId });

            builder.Entity<RoleCategory>()
                .HasOne(rs => rs.Role)
                .WithMany(r => r.RoleCategories)
                .HasForeignKey(rs => rs.RoleId);

            builder.Entity<RoleCategory>()
                .HasOne(rs => rs.Category)
                .WithMany(s => s.RoleCategories)
                .HasForeignKey(rs => rs.CategoryId);
        }
    }
}
