using System.ComponentModel.DataAnnotations;

namespace Test_02.Models;

public class Driver
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public int CarId { get; set; }

    public Car Car { get; set; } = null!;

    public ICollection<DriverCompetition> DriverCompetitions { get; set; } = new List<DriverCompetition>();

    [Timestamp] public byte[] RowVersion { get; set; } = null!;
}