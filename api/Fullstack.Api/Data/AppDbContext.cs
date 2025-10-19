using Fullstack.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Api.Data
{
    /// <summary>
    ///  DbContext : Infraestuctura de datos 
    ///  (Clean architecture : capa externa)
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Indice unic : refuerza la regla de negocio y acelera Selects
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
        }
    }
}
