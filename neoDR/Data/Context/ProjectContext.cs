using Microsoft.EntityFrameworkCore;
using neoDR.Data.Config;
using neoDR.Data.Entities;

namespace neoDR.Data.Context
{
    public class ProjectContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(20);
            modelBuilder.Entity<User>().HasKey(x => x.Id);


            modelBuilder.ApplyConfiguration(new RoleConfig());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-FKI0GRK;Database=neoDRdb;Trusted_Connection=True;TrustServerCertificate=True");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
