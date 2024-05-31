using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;
using VeterinaryClinic.DTO;
using System.Net;
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
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals(string queryBy = "name")
        {
            if (!IsValidQueryBy(queryBy.ToLower()))
            {
                return BadRequest("The Provided QueryBy Parameter Is Invalid! Please Use One Of The Following Values: name, description.");
            }

            try
            {
                var sortedAnimals = queryBy.ToLower() switch
                {
                    "description" => _context.Animals.Include(a => a.AnimalType).OrderBy(a => a.Description),
                    _ => _context.Animals.Include(a => a.AnimalType).OrderBy(a => a.Name)
                };

                return await sortedAnimals.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            try
            {
                var animal = await _context.Animals.Include(a => a.AnimalType).FirstOrDefaultAsync(a => a.Id == id);

                if (animal == null)
                {
                    return NotFound($"Animal With ID {id} Not Found!");
                }

                return Ok(animal);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> AddAnimal([FromBody] AnimalDto animalDto)
        {
            if (animalDto == null)
            {
                return BadRequest("Invalid Animal Data.");
            }

            var animalType = await _context.AnimalTypes.FirstOrDefaultAsync(at => at.Name == animalDto.AnimalType);

            if (animalType == null)
            {
                return BadRequest("The Provided AnimalType Is Not Represented In The Database.");
            }

            var animal = new Animal
            {
                Name = animalDto.Name,
                Description = animalDto.Description,
                AnimalType = animalType
            };

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] AnimalDto animalDto)
        {
            if (animalDto == null)
            {
                return BadRequest("Invalid Animal Data.");
            }

            var animal = await _context.Animals.Include(a => a.AnimalType).FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                return NotFound($"Animal With ID {id} Not Found!");
            }

            var currentRowVersion = animal.RowVersion;

            animal.Name = animalDto.Name;
            animal.Description = animalDto.Description;

            _context.Entry(animal).State = EntityState.Modified;
            _context.Entry(animal).OriginalValues["RowVersion"] = currentRowVersion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency Conflict Occurred. Please Try Again.");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound($"Animal with ID {id} not found!");
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static bool IsValidQueryBy(string queryBy)
        {
            var validParameters = new[] { "name", "description" };
            return validParameters.Contains(queryBy);
        }
    }
}
