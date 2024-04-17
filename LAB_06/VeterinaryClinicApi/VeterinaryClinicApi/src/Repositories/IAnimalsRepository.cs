using VeterinaryClinicApi.Models;

namespace VeterinaryClinicApi.Repositories
{
    public interface IAnimalsRepository
    {
        Task<List<Animal>> GetAllAnimalsAsync(string orderBy);
        Task<Animal> GetAnimalByIdAsync(int id);
        Task<Animal> AddAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(int id, Animal animal);
        Task<bool> DeleteAnimalAsync(int id);
    }
}