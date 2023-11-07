using Microsoft.AspNetCore.Mvc;
using DynamicExpressions;
using System.Net;
using BackendInterviewProblem.Models;

[ApiController]
[Route("[controller]")]
public class ProductsController : Controller
{
    private static List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1000 },
        new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 800 },
        new Product { Id = 3, Name = "Book", Category = "Books", Price = 20 },
        new Product { Id = 4, Name = "Desk Chair", Category = "Furniture", Price = 150 },
        new Product { Id = 5, Name = "Coffee Maker", Category = "Appliances", Price = 50 },
        new Product { Id = 6, Name = "Headphones", Category = "Electronics", Price = 100 },
        new Product { Id = 7, Name = "T-Shirt", Category = "Clothing", Price = 25 },
        new Product { Id = 8, Name = "Backpack", Category = "Accessories", Price = 40 },
        new Product { Id = 9, Name = "Gaming Console", Category = "Electronics", Price = 300 },
        new Product { Id = 10, Name = "Sunglasses", Category = "Accessories", Price = 50 },
        new Product { Id = 11, Name = "Desk", Category = "Furniture", Price = 200 },
        new Product { Id = 12, Name = "Plant Pot", Category = "Home Decor", Price = 15 },
        new Product { Id = 13, Name = "Running Shoes", Category = "Footwear", Price = 80 },
        new Product { Id = 14, Name = "Toaster", Category = "Appliances", Price = 30 },
        new Product { Id = 15, Name = "Dress Shirt", Category = "Clothing", Price = 45 },
    };


    [HttpGet]
    [ProducesResponseType(typeof(Pagination<Product>), (int)HttpStatusCode.OK)]
    public IActionResult GetProducts(
        string name,
        string category,
        int? minPrice,
        int? maxPrice,
        string sortOrder,
        int? pageNumber, 
        int? pageSize)
    {
        var filter = new DynamicFilterBuilder<Product>()
            .And(nameof(Product.Name), FilterOperator.ContainsIgnoreCase, name)
            .And(nameof(Product.Category), FilterOperator.ContainsIgnoreCase, category)
            .And(nameof(Product.Price), FilterOperator.GreaterThanOrEqual, minPrice ?? 0)
            .And(nameof(Product.Price), FilterOperator.LessThanOrEqual, maxPrice ?? 0)
            .Build();

        var sort = DynamicExpressions.DynamicExpressions.GetPropertyGetter<Product>(nameof(Product.Price));

        var productsQuery = Products
            .AsQueryable()
            .Where(filter);

        if (sortOrder.ToLower() == "asc")
        {
            productsQuery = productsQuery.OrderBy(sort);
        }
        else
        {
            productsQuery = productsQuery.OrderByDescending(sort);
        }

        var paginatedResult = new Pagination<Product>
        {
            CurrentPage = pageNumber ?? 1, // assuming that default pageNumber is 1,
            TotalNumberOfPages = Products.Count() / (pageSize ?? 10), // assuming that default pageSize is 10
            TotalNumberOfItems = Products.Count(),
            Data = productsQuery.ToList()
        };

        return Ok(paginatedResult);
    }
}