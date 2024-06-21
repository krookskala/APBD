using System.ComponentModel.DataAnnotations;

namespace Test_02.Models;

public class Competition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<DriverCompetition> DriverCompetitions { get; set; } = new List<DriverCompetition>();
}