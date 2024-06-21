using System.ComponentModel.DataAnnotations;

namespace Test_02.DTO
{
    public class AssingDriverCompetitionDto
    {
        [Required]
        public int DriverId { get; set; }

        [Required]
        public int CompetitionId { get; set; }
    }
}