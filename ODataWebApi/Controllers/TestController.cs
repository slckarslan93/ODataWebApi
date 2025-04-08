using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataWebApi.Context;
using ODataWebApi.Dtos;
using ODataWebApi.Models;

namespace ODataWebApi.Controllers;

//[Route("api/[controller]")]
[Route("odata")]
[ApiController]
[EnableQuery]
public sealed class TestController(AppDbContext context) : ODataController
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new();
        builder.EntitySet<Category>("Categories");
        builder.EntitySet<Product>("Products");
        return builder.GetEdmModel();
    }


    [HttpGet("categories")]
    //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Top | AllowedQueryOptions.Select | AllowedQueryOptions.Filter)]
    //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All &~ AllowedQueryOptions.Select)]
    public IQueryable<Category> Categories()
    {
        var categories = context.Categories.AsQueryable();
        return categories;
    }

    [HttpGet("Products-dto")]
    public IQueryable<ProductDto> ProductsDto()
    {
        var products = context.Products.Select(s => new ProductDto 
        { 
            Id = s.Id,
            Name = s.Name,
            Price = s.Price,
            CategoryName = s.Category != null ? s.Category.Name : string.Empty
        }).AsQueryable();
        return products;
    }


    [HttpGet("users")]
    public IActionResult Users()
    {
        var users = context.Users
            .Select(s => new UserDto
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.FullName,
                Address = s.Address,
                Id = s.Id,
                UserType = s.UserType,
                UserTypeName = s.UserType.Name,
                UserTypeValue = s.UserType.Value
            })
            .AsQueryable();
        return Ok(users);
    }
}
