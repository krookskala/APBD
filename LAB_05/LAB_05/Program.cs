using LAB_05.Database;
using LAB_05.Models;
using LAB_05.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/animals", () => StaticDatabase.Animals);
app.MapGet("/animals/{id}", (int id) => StaticDatabase.Animals.FirstOrDefault(a => a.Id == id));
app.MapPost("/animals", (Animal animal) => {
    if (!IdValidator.IsValid(animal.Id, StaticDatabase.Animals))
    {
        animal.Id = StaticDatabase.Animals.Any() ? StaticDatabase.Animals.Max(a => a.Id) + 1 : 1;
    }
    StaticDatabase.Animals.Add(animal);
    return Results.Created($"/animals/{animal.Id}", animal);
});


app.MapPut("/animals", (Animal updatedAnimal) => {
    var animal = StaticDatabase.Animals.FirstOrDefault(a => a.Id == updatedAnimal.Id);
    if (animal == null) return Results.NotFound();
    animal.Name = updatedAnimal.Name;
    animal.Category = updatedAnimal.Category;
    animal.Weight = updatedAnimal.Weight;
    animal.FurColor = updatedAnimal.FurColor;
    return Results.NoContent();
});

app.MapDelete("/animals/{id}", (int id) => {
    var animal = StaticDatabase.Animals.FirstOrDefault(a => a.Id == id);
    if (animal == null) return Results.NotFound();
    StaticDatabase.Animals.Remove(animal);
    return Results.NoContent();
});

app.MapGet("/animals/{animalId}/visits", (int animalId) => {
    var animal = StaticDatabase.Animals.FirstOrDefault(a => a.Id == animalId);
    return animal != null ? Results.Ok(animal.Visits) : Results.NotFound();
});

app.MapPost("/animals/{animalId}/visits", (int animalId, Visit visit) => {
    var animal = StaticDatabase.Animals.FirstOrDefault(a => a.Id == animalId);
    if (animal == null) return Results.NotFound($"Animal with ID {animalId} not found.");
    
    if (!IdValidator.IsValid(visit.Id, animal.Visits))
    {
        visit.Id = animal.Visits.Any() ? animal.Visits.Max(v => v.Id) + 1 : 1;
    }
    visit.AnimalId = animalId; 
    animal.Visits.Add(visit);
    return Results.Created($"/animals/{animalId}/visits/{visit.Id}", visit);
});

app.Run();