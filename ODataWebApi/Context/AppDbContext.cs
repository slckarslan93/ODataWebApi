using Microsoft.EntityFrameworkCore;
using ODataWebApi.Models;

namespace ODataWebApi.Context
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.Property(p => p.Price).HasColumnType("money");
            });
        }
    }
}