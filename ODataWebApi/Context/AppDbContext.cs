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
    }
}