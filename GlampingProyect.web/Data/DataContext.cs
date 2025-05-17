using GlampingProyect.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GlampingProyect.web.Data.Entities;  
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GlampingProyect.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Glamping> Glampings { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<GlampingRole> GlampingRole { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<RoleSection> RoleSections { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureKeys(builder);
            ConfigureIndexes(builder);

            base.OnModelCreating(builder);
        }

        private void ConfigureIndexes(ModelBuilder builder)
        {
            //Roles
            builder.Entity<GlampingRole>().HasIndex(r => r.Name)
                .IsUnique();

            //Sections
            builder.Entity<Section>().HasIndex(r => r.Name)
                .IsUnique();

            //Roles
            builder.Entity<User>().HasIndex(r => r.Document)
                .IsUnique();
        }

        private void ConfigureKeys(ModelBuilder builder)
        {
           // Role Permission
            builder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId });

            builder.Entity<RolePermission>().HasOne(rp => rp.Role)
                                            .WithMany(r => r.RolePermissions)
                                            .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>().HasOne(rp => rp.Permission)
                                            .WithMany(p => p.RolePermissions)
                                            .HasForeignKey(rp => rp.PermissionId);

            //Rol Sections
            builder.Entity<RoleSection>().HasKey(rs => new { rs.RoleId, rs.SectionId });

            builder.Entity<RoleSection>().HasOne(rs => rs.Role)
                                            .WithMany(r => r.RoleSections)
                                            .HasForeignKey(rs => rs.RoleId);

            builder.Entity<RoleSection>().HasOne(rs => rs.Section)
                                            .WithMany(s => s.RoleSections)
                                            .HasForeignKey(rs => rs.SectionId);
        }
    }
}
