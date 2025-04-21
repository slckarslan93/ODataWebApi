using Bogus;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using ODataWebApi.Context;
using ODataWebApi.Controllers;
using ODataWebApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = "Server=localhost;Database=TestODataDb;User Id=sa;Password=Admin123.;TrustServerCertificate=true";
    options.UseSqlServer(connectionString);
});

//builder.Services.AddControllers().AddOData(opt => opt.EnableQueryFeatures()); //Tum filtreleme ve s�ralama �zelliklerini a�ar

builder.Services.AddControllers().AddOData(opt =>
opt
.Select()
.Filter()
.Count()
.Expand()
.OrderBy()
.SetMaxTop(null)
.AddRouteComponents("odata", TestController.GetEdmModel())
);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();

//app.MapGet("seed-data/categories", async (AppDbContext dbContext) =>
//{
//    Faker faker = new();

//    var categoryNames = faker.Commerce.Categories(100);
//    List<Category> categories = categoryNames.Select(s => new Category
//    {
//        Name = s,
//    }).ToList();

//    dbContext.AddRange(categories);

//    await dbContext.SaveChangesAsync();

//    return Results.NoContent();
//}).Produces(204).WithTags("SeedCategories");

//app.MapGet("seed-data/products", async (AppDbContext dbContext) =>
//{
//    var categories = dbContext.Categories.ToList();

//    List<Product> products = new();
//    for (int i = 0; i < 10000; i++)
//    {
//        Faker faker = new();
//        Product product = new()
//        {
//            CategoryId = categories[new Random().Next(categories.Count)].Id,
//            Name = faker.Commerce.ProductName(),
//            Price = Convert.ToDecimal(faker.Commerce.Price(100, 1000000, 2))
//        };

//        products.Add(product);
//    }

//    dbContext.AddRange(products);

//    await dbContext.SaveChangesAsync();

//    return Results.NoContent();
//}).Produces(204).WithTags("SeedProducts");

app.MapGet("seed-data/users", async (AppDbContext dbContext) =>
{
    List<User> users = new();
    for (int i = 0; i < 10000; i++)
    {
        Faker faker = new();

        Random random = new();
        var typeValue = random.Next(0, 2);
        var userType = UserTypeEnum.FromValue(typeValue);

        User user = new()
        {
            FirstName = faker.Person.FirstName,
            LastName = faker.Person.LastName,
            UserType = userType,
            Address = new(faker.Address.City(), faker.Address.State(), faker.Address.FullAddress())
        };

        users.Add(user);
    }

    dbContext.AddRange(users);

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.MapControllers();

app.Run();