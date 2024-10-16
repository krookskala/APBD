using Microsoft.AspNetCore.Mvc;
using Test_01_Retake.Services;
using Test_01_Retake.Models;
using Test_01_Retake.DTO;

namespace Test_01_Retake.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeesController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = await _departmentService.GetDepartmentById(employeeDto.DepId);
            if (department == null)
            {
                return BadRequest("Department Not Found!");
            }

            var manager = employeeDto.ManagerId.HasValue ? await _employeeService.GetEmployeeById(employeeDto.ManagerId.Value) : null;
            if (employeeDto.ManagerId.HasValue && manager == null)
            {
                return BadRequest("Manager Not Found!");
            }

            var employee = new Employee
            {
                EmpName = employeeDto.EmpName,
                JobName = employeeDto.JobName,
                ManagerId = employeeDto.ManagerId,
                HireDate = DateTime.Now,
                Salary = employeeDto.Salary,
                Commission = employeeDto.Commission,
                DepId = employeeDto.DepId
            };
            await _employeeService.CreateEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmpId }, employee);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _employeeService.GetEmployeeById(employeeDto.EmpId);
            if (employee == null)
            {
                return NotFound("Employee Not Found!");
            }

            var department = await _departmentService.GetDepartmentById(employeeDto.DepId);
            if (department == null)
            {
                return BadRequest("Department Not Found!");
            }

            var manager = employeeDto.ManagerId.HasValue ? await _employeeService.GetEmployeeById(employeeDto.ManagerId.Value) : null;
            if (employeeDto.ManagerId.HasValue && manager == null)
            {
                return BadRequest("Manager Not Found!");
            }

            employee.EmpName = employeeDto.EmpName;
            employee.JobName = employeeDto.JobName;
            employee.ManagerId = employeeDto.ManagerId;
            employee.HireDate = employeeDto.HireDate;
            employee.Salary = employeeDto.Salary;
            employee.Commission = employeeDto.Commission;
            employee.DepId = employeeDto.DepId;

            await _employeeService.UpdateEmployee(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Employee Not Found!");
            }
            await _employeeService.RemoveEmployee(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Employee Not Found!");
            }
            return Ok(employee);
        }
    }
}
