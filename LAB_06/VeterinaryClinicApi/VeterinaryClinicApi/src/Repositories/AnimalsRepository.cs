using Microsoft.Data.SqlClient;
using VeterinaryClinicApi.Models;

namespace VeterinaryClinicApi.Repositories
{
    public class AnimalsRepository : IAnimalsRepository
    {
        private readonly string _connectionString;

        public AnimalsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Animal>> GetAllAnimalsAsync(string orderBy)
        {
            var animals = new List<Animal>();
            var query = $"SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY {orderBy} ASC;";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animals.Add(new Animal
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Category = reader.GetString(3),
                                Area = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return animals;
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            var query = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal WHERE IdAnimal = @id;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Animal
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Category = reader.GetString(3),
                                Area = reader.GetString(4)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Animal> AddAnimalAsync(Animal animal)
        {
            var query =
                "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area); SELECT SCOPE_IDENTITY();";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", animal.Name);
                    command.Parameters.AddWithValue("@Description", (object)animal.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Category", animal.Category);
                    command.Parameters.AddWithValue("@Area", animal.Area);

                    var id = await command.ExecuteScalarAsync();
                    if (id != null && int.TryParse(id.ToString(), out int generatedId))
                    {
                        animal.Id = generatedId;
                    }
                }
            }

            return animal;
        }


        public async Task<bool> UpdateAnimalAsync(int id, Animal animal)
        {
            var query =
                "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAnimal", id);
                    command.Parameters.AddWithValue("@Name", animal.Name);
                    command.Parameters.AddWithValue("@Description", animal.Description);
                    command.Parameters.AddWithValue("@Category", animal.Category);
                    command.Parameters.AddWithValue("@Area", animal.Area);

                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal;";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAnimal", id);
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
        }
    }
}