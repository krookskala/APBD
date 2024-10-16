using System.Data.SqlClient;
using Test_01_Retake.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Test_01_Retake.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(IConfiguration configuration, ILogger<EmployeeRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task CreateEmployee(Employee employee)
        {
            var query = @"
                INSERT INTO Employees (EmpName, JobName, ManagerId, HireDate, Salary, Commission, DepId)
                VALUES (@EmpName, @JobName, @ManagerId, @HireDate, @Salary, @Commission, @DepId)";

            try
            {
                await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                await using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpName", employee.EmpName);
                command.Parameters.AddWithValue("@JobName", employee.JobName);
                command.Parameters.AddWithValue("@ManagerId", employee.ManagerId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@Commission", employee.Commission ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DepId", employee.DepId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred While Creating Employee.");
                throw;
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var query = @"
                UPDATE Employees SET 
                    EmpName = @EmpName, 
                    JobName = @JobName, 
                    ManagerId = @ManagerId, 
                    HireDate = @HireDate, 
                    Salary = @Salary, 
                    Commission = @Commission, 
                    DepId = @DepId
                WHERE EmpId = @EmpId";

            try
            {
                await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                await using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpId", employee.EmpId);
                command.Parameters.AddWithValue("@EmpName", employee.EmpName);
                command.Parameters.AddWithValue("@JobName", employee.JobName);
                command.Parameters.AddWithValue("@ManagerId", employee.ManagerId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@Commission", employee.Commission ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DepId", employee.DepId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred While Updating Employee.");
                throw;
            }
        }

        public async Task RemoveEmployee(int id)
        {
            var query = "DELETE FROM Employees WHERE EmpId = @EmpId";

            try
            {
                await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                await using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpId", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred While Removing Employee.");
                throw;
            }
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            Employee? employee = null;
            var query = "SELECT * FROM Employees WHERE EmpId = @EmpId";

            try
            {
                await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                await using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpId", id);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    employee = new Employee
                    {
                        EmpId = reader.GetInt32(reader.GetOrdinal("EmpId")),
                        EmpName = reader.GetString(reader.GetOrdinal("EmpName")),
                        JobName = reader.GetString(reader.GetOrdinal("JobName")),
                        ManagerId = reader.IsDBNull(reader.GetOrdinal("ManagerId")) ? null : reader.GetInt32(reader.GetOrdinal("ManagerId")),
                        HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                        Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                        Commission = reader.IsDBNull(reader.GetOrdinal("Commission")) ? null : reader.GetDecimal(reader.GetOrdinal("Commission")),
                        DepId = reader.GetInt32(reader.GetOrdinal("DepId")),
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occurred While Fetching Employee by Id.");
                throw;
            }

            return employee;
        }
    }
}
