using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Controllers;
using WarehouseAPI.Interfaces;
using WarehouseAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Warehouse API", Version = "v1" });
});

builder.Services.AddDbContext<WarehouseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse API V1");
    c.RoutePrefix = "swagger"; 
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); 

app.Run();