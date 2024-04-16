using VeterinaryClinicApi.Models;
using VeterinaryClinicApi.Repositories;

namespace VeterinaryClinicApi.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IAnimalsRepository _animalsRepository;

        public AnimalsService(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public async Task<List<Animal>> GetAllAnimalsAsync(string orderBy = "Name")
        {
            return await _animalsRepository.GetAllAnimalsAsync(orderBy);
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            return await _animalsRepository.GetAnimalByIdAsync(id);
        }

        public async Task<Animal> AddAnimalAsync(Animal animal)
        {
            return await _animalsRepository.AddAnimalAsync(animal);
        }

        public async Task<bool> UpdateAnimalAsync(int id, Animal animal)
        {
            return await _animalsRepository.UpdateAnimalAsync(id, animal);
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            return await _animalsRepository.DeleteAnimalAsync(id);
        }
    }
}