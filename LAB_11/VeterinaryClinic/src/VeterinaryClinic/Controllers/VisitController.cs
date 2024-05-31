using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;
using VeterinaryClinic.DTO;
using VeterinaryClinic.Services;

namespace VeterinaryClinic.Controllers
{
    [Route("api/visits")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly AnimalContext _context;

        public VisitController(AnimalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetVisits()
        {
            var visits = await _context.Visits
                .Include(v => v.Employee)
                .Include(v => v.Animal)
                .OrderBy(v => v.Date)
                .ToListAsync();

            return Ok(visits.Select(v => new VisitDto
            {
                Id = v.Id,
                EmployeeName = v.Employee.Name,
                AnimalName = v.Animal.Name,
                Date = v.Date
            }));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VisitDto>> GetVisit(int id)
        {
            var visit = await _context.Visits
                .Include(v => v.Employee)
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (visit == null)
            {
                return NotFound($"Visit With ID {id} Not Found!");
            }

            var visitDto = new VisitDto
            {
                Id = visit.Id,
                EmployeeName = visit.Employee.Name,
                AnimalName = visit.Animal.Name,
                Date = visit.Date
            };

            return Ok(visitDto);
        }

        [HttpPost]
        public async Task<ActionResult<VisitDto>> AddVisit([FromBody] VisitCreateDto visitCreateDto)
        {
            if (visitCreateDto == null)
            {
                return BadRequest("Invalid visit data.");
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == visitCreateDto.EmployeeId);
            var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == visitCreateDto.AnimalId);

            if (employee == null || animal == null)
            {
                return BadRequest("The Provided EmployeeId or AnimalId Is Not Represented In The Database.");
            }

            var visit = new Visit
            {
                EmployeeId = visitCreateDto.EmployeeId,
                AnimalId = visitCreateDto.AnimalId,
                Date = visitCreateDto.Date
            };

            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();

            var visitDto = new VisitDto
            {
                Id = visit.Id,
                EmployeeName = employee.Name,
                AnimalName = animal.Name,
                Date = visit.Date
            };

            return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, visitDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVisit(int id, [FromBody] VisitUpdateDto visitUpdateDto)
        {
            if (visitUpdateDto == null)
            {
                return BadRequest("Invalid visit data.");
            }

            var visit = await _context.Visits.FirstOrDefaultAsync(v => v.Id == id);

            if (visit == null)
            {
                return NotFound($"Visit With ID {id} Not Found!");
            }

            visit.Date = visitUpdateDto.Date;

            _context.Entry(visit).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            var visit = await _context.Visits.FindAsync(id);

            if (visit == null)
            {
                return NotFound($"Visit With ID {id} Not Found!");
            }

            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(v => v.Id == id);
        }
    }
}
