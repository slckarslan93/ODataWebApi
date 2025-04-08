using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODataWebApi.Context;
using ODataWebApi.Models;

namespace ODataWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class TestController(AppDbContext context) : ControllerBase
{
    [HttpGet("categories")]
    public List<Category> Categories() 
    {
        var categories =context.Categories.ToList();
        return categories;
    }
}
