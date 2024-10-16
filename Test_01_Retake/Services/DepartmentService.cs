using Test_01_Retake.Models;
using Test_01_Retake.Repositories;
using Test_01_Retake.Services;

namespace Test_01_Retake.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<IEnumerable<Department>> GetAllDepartments()
        {
            return _departmentRepository.GetAllDepartments();
        }

        public Task<Department?> GetDepartmentById(int id)
        {
            return _departmentRepository.GetDepartmentById(id);
        }

        public Task CreateDepartment(Department department)
        {
            return _departmentRepository.CreateDepartment(department);
        }
    }
}