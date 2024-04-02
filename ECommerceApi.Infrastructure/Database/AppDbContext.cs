using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Id)
                .HasColumnName("ProductCode");

                p.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            });

            builder.Entity<Category>(c =>
            {
                c.HasKey(c => c.Id);

                c.HasMany(c => c.Products)
                .WithOne(p => p.Category);
            });

            // To make All Relationships OnDelete : Restrict
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
