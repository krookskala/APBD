using Test_01_Retake.Models;

namespace Test_01_Retake.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllDepartments();
    Task<Department?> GetDepartmentById(int id);
    Task CreateDepartment(Department department);
}
