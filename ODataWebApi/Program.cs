using Bogus;
using Microsoft.EntityFrameworkCore;
using ODataWebApi.Context;
using ODataWebApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = "Server=localhost;Database=TestODataDb;User Id=sa;Password=Admin123.;TrustServerCertificate=true";   
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();

builder.Services.AddOpenApi();


var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();

app.MapGet("seed-data/categories", async (AppDbContext dbContext) =>
{
    Faker faker = new();

    var categoryNames = faker.Commerce.Categories(100);
    List<Category> categories = categoryNames.Select(s => new Category
    {
        Name = s,
    }).ToList();

    dbContext.AddRange(categories);

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.MapControllers();

app.Run();
