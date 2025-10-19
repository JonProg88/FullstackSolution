namespace Fullstack.Api.Models
{
    /// <summary>
    // Entidad simple de dominio. Mantenerla
    // sin logica pesada (KISS)
    // y con la responsabilidad (S de SOLID)
    /// </summary>

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // DRY: valor por defecto centralizado
    }
}
