using MobileOperatorApi.Data;
using Test_01.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<OperatorRepository>();
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

// Define API endpoints directly using minimal APIs
app.MapPost("/api/mobile", async (Client client, OperatorRepository dbHelper) => 
{
    if (!client.MobileNumber.StartsWith("+48"))
        return Results.BadRequest("Mobile Number Must Start With +48.");

    int result = await dbHelper.AddOrUpdateClientAsync(client);
    if (result > 0)
        return Results.Ok();
    else
        return Results.BadRequest("Failed To Add Or Update Client.");
});

app.MapGet("/api/mobile/{mobileNumber}", async (string mobileNumber, OperatorRepository dbHelper) => 
{
    var client = await dbHelper.GetClientByMobileNumberAsync(mobileNumber);
    if (client == null) return Results.NotFound("Client Not Found.");

    return Results.Ok(client);
});

app.MapDelete("/api/mobile/{mobileNumber}", async (string mobileNumber, OperatorRepository dbHelper) => 
{
    bool deleted = await dbHelper.DeleteClientByMobileNumberAsync(mobileNumber);
    if (!deleted) return Results.NotFound("Client Not Found.");

    return Results.Ok("Client Deleted Successfully.");
});

app.Run();