using System.Data.SqlClient;
using Warehouse.Warehouse.Entity;
using Warehouse.Warehouse.Interface;
using Warehouse.Product.Entity;

namespace Warehouse.Warehouse.Repository;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;
    
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public WarehouseEntity Get(long id)
    {
        var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdWarehouse, Name, Address FROM Warehouse WHERE IdWarehouse = @Id";
        cmd.Parameters.AddWithValue("@Id", id);
        
        var dr = cmd.ExecuteReader();
        
        WarehouseEntity warehouse = null;
        if (dr.Read())
        {
            warehouse = new WarehouseEntity
            {
                IdWarehouse = (int)dr["IdWarehouse"],
                Name = dr["Name"].ToString(),
                Address = dr["Address"].ToString()
            };
        }
        
        con.Close();
        
        return warehouse;
    }
}