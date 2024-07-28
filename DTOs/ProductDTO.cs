using System.ComponentModel.DataAnnotations;

namespace ProductsCategoryAPI.DTOs
{
    public class ProductDTO
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Name { get; set; }
        [Required]
        [StringLength(200)]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
