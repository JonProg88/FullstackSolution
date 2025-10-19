using System.ComponentModel.DataAnnotations;

namespace Fullstack.Api.DTOs
{
    /// <summary>
    ///  DTO de entrada : contrato estable hacia el cliente, evita filtrar la entidad 
    /// </summary>
    public class ProductCreateDto
    {
        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 999999)]
        public decimal Price { get; set; }
        [Range(0.01, 1000000)]
        public int Stock { get;set; }
    }
}
