using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_02.DTO;
using Test_02.Models;

namespace Test_02.Controllers;

[Route("api/driver/competitions")]
[ApiController]
public class DriverCompetitionsController : ControllerBase
{
    private readonly RacingTournamentContext _context;

    public DriverCompetitionsController(RacingTournamentContext context)
    {
        _context = context;
    }

    [HttpGet("{driverId}")]
    public async Task<ActionResult<IEnumerable<Competition>>> GetDriverCompetitions(int driverId)
    {
        var driverCompetitions = await _context.DriverCompetitions
            .Where(dc => dc.DriverId == driverId)
            .Include(dc => dc.Competition)
            .Select(dc => dc.Competition)
            .ToListAsync();

        if (!driverCompetitions.Any())
        {
            return NotFound(new { Message = "No Competitions Found For The Driver!" });
        }

        return Ok(driverCompetitions);
    }

    [HttpPost]
    public async Task<IActionResult> AssignDriverToCompetition([FromBody] AssingDriverCompetitionDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var driver = await _context.Drivers.FindAsync(model.DriverId);
        var competition = await _context.Competitions.FindAsync(model.CompetitionId);

        if (driver == null || competition == null)
        {
            return NotFound(new { Message = "Driver Or Competition Not Found!" });
        }

        var driverCompetition = new DriverCompetition
        {
            DriverId = model.DriverId,
            CompetitionId = model.CompetitionId,
            Date = DateTime.Now
        };

        _context.DriverCompetitions.Add(driverCompetition);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}