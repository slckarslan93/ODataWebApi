﻿namespace ODataWebApi.Models;

public sealed class Product
{
    public Product()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public decimal Price { get; set; }
}