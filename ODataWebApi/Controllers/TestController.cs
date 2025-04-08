using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataWebApi.Context;
using ODataWebApi.Models;

namespace ODataWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class TestController(AppDbContext context) : ControllerBase
{
    [HttpGet("categories")]
    [EnableQuery]
    public IQueryable<Category> Categories() 
    {
        var categories =context.Categories.AsQueryable();
        return categories;
    }
}
