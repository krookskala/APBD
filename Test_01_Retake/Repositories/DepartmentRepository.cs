using System.Data.SqlClient;
using Test_01_Retake.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Test_01_Retake.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(IConfiguration configuration, ILogger<DepartmentRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            var departments = new List<Department>();
            var query = "SELECT * FROM Department";

            try
            {
                await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                await using var command = new SqlCommand(query, connection);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    departments.Add(new Department
                    {
                        DepId = reader.GetInt32(reader.GetOrdinal("DepId")),
                        DepName = reader.GetString(reader.GetOrdinal("DepName")),
                        DepLocation = reader.GetString(reader.GetOrdinal("DepLocation")),
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred While Fetching Departments.");
                throw;
            }

            return departments;
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            Department? department = null;
            var query = "SELECT * FROM Department WHERE DepId = @DepId";

            try
            {
                await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                await using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DepId", id);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    department = new Department
                    {
                        DepId = reader.GetInt32(reader.GetOrdinal("DepId")),
                        DepName = reader.GetString(reader.GetOrdinal("DepName")),
                        DepLocation = reader.GetString(reader.GetOrdinal("DepLocation")),
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred While Fetching Department by Id.");
                throw;
            }

            return department;
        }

        public async Task CreateDepartment(Department department)
        {
            var query = "INSERT INTO Department (DepName, DepLocation) VALUES (@DepName, @DepLocation)";

            try
            {
                await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                await using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DepName", department.DepName);
                command.Parameters.AddWithValue("@DepLocation", department.DepLocation);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred While Creating Department.");
                throw;
            }
        }
    }
}
