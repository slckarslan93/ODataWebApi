using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataWebApi.Context;
using ODataWebApi.Models;

namespace ODataWebApi.Controllers;

//[Route("api/[controller]")]
[Route("odata")]
[ApiController]
[EnableQuery]
public sealed class TestController(AppDbContext context) : ControllerBase
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

    [HttpGet("Products")]
    public IQueryable<Product> Products()
    {
        var products = context.Products.AsQueryable();
        return products;
    }
}
