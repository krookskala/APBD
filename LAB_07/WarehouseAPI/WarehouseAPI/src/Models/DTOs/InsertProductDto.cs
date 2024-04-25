using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Models.DTOs
{
    public class InsertProductDto
    {
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int IdWarehouse { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public int Amount { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}