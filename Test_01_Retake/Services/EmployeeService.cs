using Test_01_Retake.Models;
using Test_01_Retake.Repositories;
using Test_01_Retake.Services;

namespace Test_01_Retake.Services
{
    public class EmployeeService : IEmployeeService 
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task CreateEmployee(Employee employee)
        {
            return _employeeRepository.CreateEmployee(employee);
        }

        public Task UpdateEmployee(Employee employee)
        {
            return _employeeRepository.UpdateEmployee(employee);
        }

        public Task RemoveEmployee(int id)
        {
            return _employeeRepository.RemoveEmployee(id);
        }

        public Task<Employee?> GetEmployeeById(int id)
        {
            return _employeeRepository.GetEmployeeById(id);
        }
    }
}