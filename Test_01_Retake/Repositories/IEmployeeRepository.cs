using Test_01_Retake.Models;

namespace Test_01_Retake.Repositories;

public interface IEmployeeRepository
{
    Task CreateEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task RemoveEmployee(int id);
    Task<Employee?> GetEmployeeById(int id);
}
