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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.Property(p => p.Price).HasColumnType("money");
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(p => p.UserType)
                .HasConversion(type => type.Value, value => UserTypeEnum.FromValue(value)); //db ye enum valuesini kaydet donuste vallue ya gore uygun enumi don

                builder.OwnsOne(p => p.Address); //Direkt adress olarak eklemiyor edressin altindaki propertyleride db ye eklemis oluyor addres_City, addres_Town, addres_FullAddress gibi
            });
        }
    }
}