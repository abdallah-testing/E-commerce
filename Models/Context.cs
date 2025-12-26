using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public Context(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p =>
            {
                p.HasIndex(p => p.SKU)
                .IsUnique();

                p.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(u =>
            {
                u.HasIndex(u => u.Username).IsUnique();
                u.HasIndex(u => u.Email).IsUnique();
                u.Property(u => u.Id).ValueGeneratedOnAdd();
            });

            string PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123");

            modelBuilder.Entity<User>(u =>
            {
                u.Property(u => u.Role)
                .HasConversion<string>();

                u.HasData(new User
                {
                    Id = 1,
                    Username = "Admin",
                    Password = PasswordHash,
                    Email = "Admin@gmail.com",
                    Role = Role.Admin,
                    CreatedAt = DateTime.Now
                });
            });
        }
    }
}
