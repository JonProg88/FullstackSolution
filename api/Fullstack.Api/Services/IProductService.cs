using Fullstack.Api.DTOs;

namespace Fullstack.Api.Services
{
    /// <summary>
    /// Interfaz para inversion de dependencias (D de SOLID)
    /// Facilita pruebas y remplazos
    /// </summary>
    public interface IProductService
    {
        Task<(IEnumerable<ProductReadDto> items, int total)> ListAsync(int page, int size);
        Task<ProductReadDto?> GetAsync(int id);
        Task<int> CreateAsync(ProductCreateDto dto);
        Task<bool> UpdateAsync(int id, ProductCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
