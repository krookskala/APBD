using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_02.DTO;
using Test_02.Models;

namespace Test_02.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly RacingTournamentContext _context;

    public DriversController(RacingTournamentContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers(string sortBy = "FirstName")
    {
        var drivers = _context.Drivers.Include(d => d.Car).ThenInclude(c => c.CarManufacturer).AsQueryable();

        switch (sortBy.ToLower())
        {
            case "lastname":
                drivers = drivers.OrderBy(d => d.LastName);
                break;
            case "birthday":
                drivers = drivers.OrderBy(d => d.Birthday);
                break;
            default:
                drivers = drivers.OrderBy(d => d.FirstName);
                break;
        }

        return Ok(await drivers.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDetailDto>> GetDriver(int id)
    {
        var driver = await _context.Drivers
            .Include(d => d.Car)
            .ThenInclude(c => c.CarManufacturer)
            .Where(d => d.Id == id)
            .Select(d => new DriverDetailDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Birthday = d.Birthday,
                CarNumber = d.Car.Number,
                CarManufacturerName = d.Car.CarManufacturer.Name,
                CarModelName = d.Car.ModelName
            })
            .FirstOrDefaultAsync();

        if (driver == null)
        {
            return NotFound(new { Message = "Driver Not Found!" });
        }

        return Ok(driver);
    }


    [HttpPost]
    public async Task<ActionResult<Driver>> PostDriver([FromBody] DriverCreateDto driverCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var driver = new Driver
        {
            FirstName = driverCreateDto.FirstName,
            LastName = driverCreateDto.LastName,
            Birthday = driverCreateDto.Birthday,
            CarId = driverCreateDto.CarId
        };

        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
    }
}
