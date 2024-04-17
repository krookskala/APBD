using VeterinaryClinicApi.Models;

namespace VeterinaryClinicApi.Services
{
    public interface IAnimalsService
    {
        Task<List<Animal>> GetAllAnimalsAsync(string orderBy = "Name");
        Task<Animal> GetAnimalByIdAsync(int id);
        Task<Animal> AddAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(int id, Animal animal);
        Task<bool> DeleteAnimalAsync(int id);
    }
}