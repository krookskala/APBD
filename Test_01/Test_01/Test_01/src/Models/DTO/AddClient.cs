using System.ComponentModel.DataAnnotations;

namespace Test_01.Models.DTO
{
    
    public class AddClient
    {
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }

}