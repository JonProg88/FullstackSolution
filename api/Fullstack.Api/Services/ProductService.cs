using Fullstack.Api.Data;
using Fullstack.Api.DTOs;
using Fullstack.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Api.Services
{
    /// <summary>
    /// // Implementación simple: lógica de aplicación 
    /// sin mezclar controladores/endpoints (KISS/DRY)
    /// </summary>
    public class ProductService(AppDbContext db) : IProductService
    {
        public async Task<(IEnumerable<ProductReadDto> items, int total)> ListAsync(int page, int size)
        {
            if (page < 1) page = 1; if (size < 1) size = 10;
            var query = db.Products.AsNoTracking().OrderByDescending(x => x.Id);
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size)
            .Select(p => new ProductReadDto { Id = p.Id, Name = p.Name, Price = p.Price, Stock = p.Stock, CreatedAt = p.CreatedAt })
            .ToListAsync();
            return (items, total);
        }

        public async Task<ProductReadDto?> GetAsync(int id)
        {
            var p = await db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return p is null ? null : new ProductReadDto { Id = p.Id, Name = p.Name, Price = p.Price, Stock = p.Stock, CreatedAt = p.CreatedAt };
        }

        public async Task<int> CreateAsync(ProductCreateDto dto)
        {
            // Evita duplicados (regla de negocio)
            var exists = await db.Products.AnyAsync(x => x.Name == dto.Name);
            if (exists) throw new InvalidOperationException("Duplicado: Name ya existe");


            // Opción A: EF normal
            var p = new Product { Name = dto.Name, Price = dto.Price, Stock = dto.Stock };
            db.Add(p);
            await db.SaveChangesAsync();
            return p.Id;


            // Opción B: usar SP (sql/init.sql) con ExecuteSqlInterpolated si lo piden
            // var id = await db.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.SpCreateProduct {dto.Name}, {dto.Price}, {dto.Stock}");
        }

        public async Task<bool> UpdateAsync(int id, ProductCreateDto dto)
        {
            var p = await db.Products.FindAsync(id);
            if (p is null) return false;
            var dup = await db.Products.AnyAsync(x => x.Name == dto.Name && x.Id != id);
            if (dup) throw new InvalidOperationException("Duplicado: Name ya existe");
            p.Name = dto.Name; p.Price = dto.Price; p.Stock = dto.Stock;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await db.Products.FindAsync(id);
            if (p is null) return false;
            db.Remove(p);
            await db.SaveChangesAsync();
            return true;
        }

    }
}
