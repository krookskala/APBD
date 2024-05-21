using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;
using VeterinaryClinic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AnimalContext>(options =>
    options.UseSqlServer(
        "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True;"));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/animals", (AnimalContext context) => context.Animals.ToList());
app.MapPost("/animals", (AnimalContext context, [FromBody] Animal animal) =>
{
    context.Entry(animal).State = EntityState.Modified;
    context.SaveChanges();
});

app.Run();