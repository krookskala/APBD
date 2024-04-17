using VeterinaryClinicApi.Repositories;
using VeterinaryClinicApi.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("VeterinaryDb");
if (connectionString == null)
{
    throw new InvalidOperationException("Connection String 'VeterinaryDb' Not Found in appsettings.json");
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IAnimalsRepository>(provider => new AnimalsRepository(connectionString));
builder.Services.AddScoped<IAnimalsService, AnimalsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();