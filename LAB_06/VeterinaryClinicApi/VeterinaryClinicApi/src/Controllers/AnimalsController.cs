using Microsoft.AspNetCore.Mvc;
using VeterinaryClinicApi.Models;
using VeterinaryClinicApi.Services;

namespace VeterinaryClinicApi.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsService _animalsService;

        public AnimalsController(IAnimalsService animalsService)
        {
            _animalsService = animalsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnimals([FromQuery] string orderBy = "Name")
        {
            var animals = await _animalsService.GetAllAnimalsAsync(orderBy);
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimalById(int id)
        {
            var animal = await _animalsService.GetAnimalByIdAsync(id);
            if (animal == null)
                return NotFound($"Animal with ID {id} is not found");
            return Ok(animal);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal([FromBody] Animal animal)
        {
            var addedAnimal = await _animalsService.AddAnimalAsync(animal);
            return CreatedAtAction(nameof(GetAnimalById), new { id = addedAnimal.Id }, addedAnimal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal animal)
        {
            await _animalsService.UpdateAnimalAsync(id, animal);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var deleted = await _animalsService.DeleteAnimalAsync(id);
            if (!deleted)
                return NotFound($"Animal with ID {id} is not found");

            return NoContent();
        }
    }
}