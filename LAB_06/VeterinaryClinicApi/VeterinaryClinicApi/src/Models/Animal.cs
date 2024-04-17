namespace VeterinaryClinicApi.Models;

public class Animal
{
    public int IdAnimal { get; set; }
    public string Name { get; set; } = string.Empty; 
    public string? Description { get; set; } 
    public string Category { get; set; } = string.Empty; 
    public string Area { get; set; } = string.Empty; 
}