using System.Data.SqlClient;
using System.Data.SqlTypes;
using Warehouse.Product.Entity;
using Warehouse.Product.Interface;

namespace Warehouse.Product.Repository;

public class ProductRepository : IProductRepository
{
    private IConfiguration _configuration;
    
    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ProductEntity? Get(int id)
    {
        var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdProduct, Name, Description, Price FROM Product WHERE IdProduct = @Id";
        cmd.Parameters.AddWithValue("@Id", id);
        
        var dr = cmd.ExecuteReader();
        
        ProductEntity product = null;
        if (dr.Read())
        {
            product = new ProductEntity
            {
                Id = (int)dr["IdProduct"],
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Price = Convert.ToDouble(dr["Price"])
            };
        }
        
        con.Close();
        
        return product;
    }
}