var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapPost("/api/auth/register", async (CreateUserDto createUserDto, IUserService userService, CancellationToken ct) =>
{
    var result = await userService.RegisterUserAsync(createUserDto, ct);
    return Results.Ok(result);
}).AllowAnonymous();


app.MapPost("/api/auth/login", async (LoginDto loginDto, IUserService userService) =>
{
    var result = await userService.AuthenticateUserAsync(loginDto);
    return result.IsSuccess ? Results.Ok(result) : Results.Unauthorized();
}).AllowAnonymous();

app.MapPost("/api/auth/refresh", async (TokenRequestDto tokenRequest, IUserService userService) =>
{
    var result = await userService.RefreshTokenAsync(tokenRequest.RefreshToken);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest("Invalid Refresh Token");
}).AllowAnonymous();

app.MapDelete("/api/data/{id}", [Authorize(Policy = "AdminOnly")] (int id) =>
{
    return Results.NoContent();
});

app.UseMiddleware<ErrorHandlingMiddleware>(); 

app.Run();
