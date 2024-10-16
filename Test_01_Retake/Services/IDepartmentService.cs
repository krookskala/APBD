using Test_01_Retake.Models;

namespace Test_01_Retake.Services;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllDepartments();
    Task<Department?> GetDepartmentById(int id);
    Task CreateDepartment(Department department);
}
