using System.ComponentModel.DataAnnotations;

namespace ProductsCategoryAPI.DTOs
{
    public class CategoryDTO
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Name { get; set; }
        [Required]
        [StringLength(200)]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
