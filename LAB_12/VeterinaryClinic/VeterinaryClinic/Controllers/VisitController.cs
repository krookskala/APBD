using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;
using VeterinaryClinic.DTO;
using VeterinaryClinic.Services;

namespace VeterinaryClinic.Controllers
{
    [Route("api/visits")]
    [Authorize]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly AnimalContext _context;

        public VisitController(AnimalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetVisitDto>>> GetVisits(CancellationToken cancellationToken)
        {
            var visits = await _context.Visits
                .Include(v => v.Employee)
                .Include(v => v.Animal)
                .OrderBy(v => v.Date)
                .ToListAsync(cancellationToken);

            var result = visits.Select(v => new GetVisitDto
            {
                EmployeeId = v.EmployeeId,
                AnimalId = v.AnimalId,
                Date = v.Date
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetVisitDto>> GetVisit(int id, CancellationToken cancellationToken)
        {
            var visit = await _context.Visits
                .Include(v => v.Employee)
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

            if (visit == null)
            {
                return NotFound($"Visit With ID {id} Not Found!");
            }

            var visitDto = new GetVisitDto
            {
                EmployeeId = visit.EmployeeId,
                AnimalId = visit.AnimalId,
                Date = visit.Date
            };

            return Ok(visitDto);
        }

        [HttpPost]
        public async Task<ActionResult<GetVisitDto>> PostVisit([FromBody] GetVisitDto getVisitDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.FindAsync(new object[] { getVisitDto.EmployeeId }, cancellationToken);
            if (employee == null)
            {
                return BadRequest("EmployeeId Does Not Exist.");
            }

            var animal = await _context.Animals.FindAsync(new object[] { getVisitDto.AnimalId }, cancellationToken);
            if (animal == null)
            {
                return BadRequest("AnimalId Is Not Represented In The Database.");
            }

            var visit = new Visit
            {
                EmployeeId = getVisitDto.EmployeeId,
                AnimalId = getVisitDto.AnimalId,
                Date = getVisitDto.Date
            };

            _context.Visits.Add(visit);
            await _context.SaveChangesAsync(cancellationToken);

            var visitDto = new GetVisitDto
            {
                EmployeeId = visit.EmployeeId,
                AnimalId = visit.AnimalId,
                Date = visit.Date
            };

            return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, visitDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutVisit(int id, [FromBody] VisitUpdateDto visitUpdateDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var visit = await _context.Visits.FindAsync(new object[] { id }, cancellationToken);

            if (visit == null)
            {
                return NotFound($"Visit With ID {id} Not Found!");
            }

            visit.EmployeeId = visitUpdateDto.EmployeeId;
            visit.AnimalId = visitUpdateDto.AnimalId;
            visit.Date = visitUpdateDto.Date;

            _context.Entry(visit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVisit(int id, CancellationToken cancellationToken)
        {
            var visit = await _context.Visits.FindAsync(new object[] { id }, cancellationToken);

            if (visit == null)
            {
                return NotFound($"Visit With ID {id} Not Found!");
            }

            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(v => v.Id == id);
        }
    }
}
