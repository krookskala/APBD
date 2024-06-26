using System.ComponentModel.DataAnnotations;

namespace Test_02.Models;

public class DriverCompetition
{
    public int DriverId { get; set; }

    public Driver Driver { get; set; } = null!;

    public int CompetitionId { get; set; }

    public Competition Competition { get; set; } = null!;

    public DateTime Date { get; set; }
}