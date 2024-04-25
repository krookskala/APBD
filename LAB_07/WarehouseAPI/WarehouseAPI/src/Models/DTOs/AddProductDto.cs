using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Models.DTOs
{
    public class AddProductDto
    {
        [Required]
        [MaxLength(200)] 
        public string Name { get; set; }
        
        [Required]
        [MaxLength(200)] 
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
    }
}