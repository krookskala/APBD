using System.ComponentModel.DataAnnotations;

namespace Test_02.Models;

public class CarManufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}