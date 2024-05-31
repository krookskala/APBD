namespace VeterinaryClinic.Models
{
    public class AnimalTypes
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}