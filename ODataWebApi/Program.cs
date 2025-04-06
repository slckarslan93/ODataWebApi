using Bogus;
using Microsoft.EntityFrameworkCore;
using ODataWebApi.Context;
using ODataWebApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = "Data Source=localhost;Initial Catalog=TestODataDb;User ID=sa;Password=********;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";   
    options.UseSqlServer();
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
