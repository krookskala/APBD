using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Test_01.Models;
using Test_01.Models.DTO;

namespace Test_01.Services
{
    public class OperatorService : IOperatorService
    {
        private readonly string _connectionString;

        public OperatorService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddOrUpdateClientAndPhoneNumberAsync(ClientPhoneNumberDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            var command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                int clientId;
                command.CommandText = @"
                    IF EXISTS (SELECT 1 FROM Client WHERE Email = @Email)
                    BEGIN
                        UPDATE Client SET Fullname = @Fullname, City = @City WHERE Email = @Email;
                        SELECT Id FROM Client WHERE Email = @Email;  
                    END
                    ELSE
                    BEGIN
                        INSERT INTO Client (Fullname, Email, City) VALUES (@Fullname, @Email, @City);
                        SELECT CAST(scope_identity() AS int);  
                    END";

                command.Parameters.AddWithValue("@Fullname", dto.Fullname);
                command.Parameters.AddWithValue("@Email", dto.Email);
                command.Parameters.AddWithValue("@City", dto.City ?? (object)DBNull.Value);

                clientId = (int)await command.ExecuteScalarAsync();
                
                command.Parameters.Clear();
                command.CommandText = @"
                    INSERT INTO PhoneNumber (Operator_Id, Client_Id, Number)
                    VALUES ((SELECT Id FROM Operator WHERE Name = @OperatorName), @ClientId, @PhoneNumber);
                    SELECT CAST(scope_identity() AS int);"; 

                command.Parameters.AddWithValue("@OperatorName", dto.OperatorName);
                command.Parameters.AddWithValue("@ClientId", clientId);
                command.Parameters.AddWithValue("@PhoneNumber", dto.PhoneNumber);

                var phoneNumberId = (int)await command.ExecuteScalarAsync();

                await transaction.CommitAsync();
                return phoneNumberId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Database operation failed: " + ex.Message);
            }
        }

        public async Task<ClientPhoneNumberDetails> GetClientDetailsByPhoneNumberAsync(string phoneNumber)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand(@"
                SELECT p.Number, c.Fullname, c.Email, c.City, o.Name AS OperatorName
                FROM PhoneNumber p
                JOIN Client c ON p.Client_Id = c.Id
                JOIN Operator o ON p.Operator_Id = o.Id
                WHERE p.Number = @PhoneNumber", connection);

            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                return new ClientPhoneNumberDetails
                {
                    PhoneNumber = reader["Number"].ToString(),
                    Fullname = reader["Fullname"].ToString(),
                    Email = reader["Email"].ToString(),
                    City = reader["City"].ToString(),
                    OperatorName = reader["OperatorName"].ToString()
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeletePhoneNumberAndClientAsync(string phoneNumber)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            var command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "SELECT Client_Id FROM PhoneNumber WHERE Number = @PhoneNumber";
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                var clientId = (int?)await command.ExecuteScalarAsync();

                if (clientId == null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
                command.CommandText = "DELETE FROM PhoneNumber WHERE Number = @PhoneNumber";
                await command.ExecuteNonQueryAsync();

                command.CommandText = "SELECT COUNT(*) FROM PhoneNumber WHERE Client_Id = @ClientId";
                command.Parameters.AddWithValue("@ClientId", clientId.Value);
                int count = (int)await command.ExecuteScalarAsync();

                if (count == 0)
                {
                    command.CommandText = "DELETE FROM Client WHERE Id = @ClientId";
                    await command.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}