using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Test_01_Retake.DTO
{
    public class DepartmentDto
    {
        [JsonPropertyName("Id")]
        public int DepId { get; set; }

        [JsonPropertyName("Name")]
        [Required]
        [StringLength(50)]
        public string DepName { get; set; } = string.Empty;

        [JsonPropertyName("Location")]
        [Required]
        [StringLength(50)]
        public string DepLocation { get; set; } = string.Empty;
    }
}