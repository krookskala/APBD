using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Test_01_Retake.DTO
{
    public class EmployeeDto
    {
        [JsonPropertyName("Id")]
        public int EmpId { get; set; }

        [JsonPropertyName("Name")]
        [Required]
        [StringLength(100)]
        public string EmpName { get; set; } = string.Empty;

        [JsonPropertyName("Job")]
        [Required]
        [StringLength(50)]
        public string JobName { get; set; } = string.Empty;

        [JsonPropertyName("ManagerId")]
        public int? ManagerId { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [JsonPropertyName("Salary")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [JsonPropertyName("Comission")]
        [Range(0, double.MaxValue)]
        public decimal? Commission { get; set; }

        [JsonPropertyName("DepId")]
        [Required]
        public int DepId { get; set; }
    }
}