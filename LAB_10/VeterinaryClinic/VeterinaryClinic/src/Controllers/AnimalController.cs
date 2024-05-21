using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;
using VeterinaryClinic.Models.DTO;
using VeterinaryClinic.Services;

namespace VeterinaryClinic.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly AnimalContext _context;

        public AnimalController(AnimalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals(string orderBy = "name")
        {
            if (!IsValidOrderBy(orderBy.ToLower()))
            {
                return BadRequest("The Provided orderBy Parameter Is Invalid! Please Use One Of The Following Values: Name, Description, Category or Area.");
            }

            var sorted = orderBy.ToLower() switch
            {
                "description" => _context.Animals.OrderBy(a => a.Description),
                "category" => _context.Animals.OrderBy(a => a.Category),
                "area" => _context.Animals.OrderBy(a => a.Area),
                _ => _context.Animals.OrderBy(a => a.Name)
            };

            return await sorted.ToListAsync();
        }

        private static bool IsValidOrderBy(string orderBy)
        {
            var parameters = new[] { "name", "description", "category", "area" };
            return parameters.Contains(orderBy);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            return animal == null ? NotFound($"Animal With Id {id} Not Found!") : Ok(animal);
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> AddAnimal(AnimalDto animalDto)
        {
            var animal = ConvertDtoToAnimal(animalDto);
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        private static Animal ConvertDtoToAnimal(AnimalDto animalDto)
        {
            return new Animal
            {
                Name = animalDto.Name,
                Description = string.IsNullOrEmpty(animalDto.Description) ? null : animalDto.Description,
                Category = animalDto.Category,
                Area = animalDto.Area
            };
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Animal>> UpdateAnimal(int id, AnimalDto animalDto)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return await AddAnimal(animalDto);
            }

            animal.Name = animalDto.Name;
            animal.Description = string.IsNullOrEmpty(animalDto.Description) ? null : animalDto.Description;
            animal.Category = animalDto.Category;
            animal.Area = animalDto.Area;

            _context.Entry(animal).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound($"Animal With Id {id} Not Found!");
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
