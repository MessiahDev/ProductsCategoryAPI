using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCategoryAPI.Entities
{
    [Table("Categories")]
    public class Category
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
        public ICollection<Product>? Products { get; set; }

        public Category()
        {
            Products = new Collection<Product>();
        }
    }
}
