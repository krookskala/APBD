using Microsoft.AspNetCore.Mvc;
using Test_01_Retake.Services;
using Test_01_Retake.Models;
using Test_01_Retake.DTO;

namespace Test_01_Retake.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound("Department Not Found!");
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = new Department
            {
                DepName = departmentDto.DepName,
                DepLocation = departmentDto.DepLocation
            };
            await _departmentService.CreateDepartment(department);

            // Fetch the created department to get the ID
            var createdDepartment = await _departmentService.GetDepartmentById(department.DepId);
            if (createdDepartment == null)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.DepId }, createdDepartment);
        }
    }
}