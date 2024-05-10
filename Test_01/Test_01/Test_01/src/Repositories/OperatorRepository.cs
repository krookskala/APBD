using System;
using System.Data.SqlClient;
using Test_01.Repositories;

namespace MobileOperatorApi.Data
{
    public class OperatorRepository
    {
        private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";

        public async Task<int> AddOrUpdateClientAsync(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command;
                
                command = new SqlCommand("SELECT ClientId FROM Clients WHERE Email = @Email", connection);
                command.Parameters.AddWithValue("@Email", client.Email);
                var clientId = (int?)await command.ExecuteScalarAsync();

                if (clientId.HasValue)
                {
                    command = new SqlCommand("UPDATE Clients SET FullName = @FullName, City = @City WHERE ClientId = @ClientId", connection);
                    command.Parameters.AddWithValue("@ClientId", clientId.Value);
                }
                else
                {
                    command = new SqlCommand("INSERT INTO Clients (FullName, Email, City, MobileNumber) VALUES (@FullName, @Email, @City, @MobileNumber); SELECT SCOPE_IDENTITY();", connection);
                    command.Parameters.AddWithValue("@MobileNumber", client.MobileNumber);
                }

                command.Parameters.AddWithValue("@FullName", client.FullName);
                command.Parameters.AddWithValue("@City", string.IsNullOrEmpty(client.City) ? DBNull.Value : (object)client.City);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<Client> GetClientByMobileNumberAsync(string mobileNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT ClientId, FullName, Email, City, MobileNumber FROM Clients WHERE MobileNumber = @MobileNumber", connection);
                command.Parameters.AddWithValue("@MobileNumber", mobileNumber);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new Client
                        {
                            ClientId = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Email = reader.GetString(2),
                            City = reader.IsDBNull(3) ? null : reader.GetString(3),
                            MobileNumber = reader.GetString(4)
                        };
                    }
                }
            }
            return null;
        }

        public async Task<bool> DeleteClientByMobileNumberAsync(string mobileNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Clients WHERE MobileNumber = @MobileNumber", connection);
                command.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                
                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }
    }
}
