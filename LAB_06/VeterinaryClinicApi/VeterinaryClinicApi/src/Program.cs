using VeterinaryClinicApi.Repositories;
using VeterinaryClinicApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IAnimalsRepository, AnimalsRepository>();
builder.Services.AddScoped<IAnimalsService, AnimalsService>();

var app = builder.Build();
var connectionString = app.Configuration.GetConnectionString("VeterinaryDb");

if (connectionString != null)
{
    builder.Services.AddScoped<IAnimalsRepository>(provider =>
    {
        return new AnimalsRepository(connectionString);
    });
}
else
{
    throw new InvalidOperationException("Connection String 'VeterinaryDb' Not Found in appsettings.json");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();