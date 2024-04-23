using Warehouse.Product.Interface;
using Warehouse.Product.Repository;
using Warehouse.ProductWarehouse.Interface;
using Warehouse.ProductWarehouse.Repository;
using Warehouse.ProductWarehouse.Service;

namespace Warehouse;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductWarehouseRepository, ProductWarehouseRepository>();
        
        builder.Services.AddScoped<IProductWarehouseService, ProductWarehouseService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
        app.UseHttpsRedirection();
        app.Run();
    }
}