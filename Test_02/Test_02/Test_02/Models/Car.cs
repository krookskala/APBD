using System.ComponentModel.DataAnnotations;

namespace Test_02.Models;

public class Car
{
    public int Id { get; set; }

    public int CarManufacturerId { get; set; }

    public CarManufacturer CarManufacturer { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public int Number { get; set; }

    public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}