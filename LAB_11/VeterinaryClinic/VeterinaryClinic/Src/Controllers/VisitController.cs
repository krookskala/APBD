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
                .Select(v => new VisitDto
                {
                    Id = v.Id,
                    EmployeeName = v.Employee.Name,
                    AnimalName = v.Animal.Name,
                    Date = v.Date
                })
                .ToListAsync();

            return Ok(visits);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Visit>> GetVisit(int id)
        {
            var visit = await _context.Visits
                .Include(v => v.Employee)
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (visit == null)
            {
                return NotFound($"Visit With ID {id} Not Found!");
            }

            return Ok(new VisitDto
            {
                Id = visit.Id,
                EmployeeName = visit.Employee.Name,
                AnimalName = visit.Animal.Name,
                Date = visit.Date
            });
        }
        
        [HttpPost]
        public async Task<ActionResult<Visit>> AddVisit([FromBody] VisitCreateDto visitDto)
        {
            var employee = await _context.Employees.FindAsync(visitDto.EmployeeId);
            var animal = await _context.Animals.FindAsync(visitDto.AnimalId);

            if (employee == null || animal == null)
            {
                return BadRequest("The Provided EmployeeId Or AnimalId Is Not Represented In The Database.");
            }

            var visit = new Visit
            {
                EmployeeId = visitDto.EmployeeId,
                AnimalId = visitDto.AnimalId,
                Date = visitDto.Date
            };

            _context.Visits.Add(visit);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, visit);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVisit(int id, [FromBody] VisitUpdateDto visitDto)
        {
            var visit = await _context.Visits.FindAsync(id);

            if (visit == null)
            {
                return NotFound($"Visit With ID {id} Not Found!");
            }

            visit.Date = visitDto.Date;

            _context.Entry(visit).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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
    }
}