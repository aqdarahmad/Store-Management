using Microsoft.AspNetCore.Mvc;
using StoreManagment.Data;
using StoreManagment.Models;
using StoreManagment.DTO;
using Microsoft.AspNetCore.Authorization;

namespace StoreManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpGet]

    public IActionResult GetAllProduct( [FromQuery] string? name , [FromQuery] decimal? minprice, [FromQuery] decimal? maxprice, [FromQuery] Guid? categoryId , [FromQuery] int pagenumber = 1, [FromQuery] int pagesize = 10 )

    {
        if (pagenumber < 1 || pagesize < 1 || pagesize > 50)
            return BadRequest("Invalid pagenumber or pagesize");


        if (minprice.HasValue && maxprice.HasValue && minprice.Value > maxprice.Value)
            return BadRequest("Min price can not exceed max price");


        if(maxprice < 0 || minprice < 0)
            return BadRequest("Price can't be negative");




        var query = _context.Products.OrderBy(p => p.Name).AsQueryable();
        // filter by product name 
        if(!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        if(minprice.HasValue)
        {
            query = query.Where(p => p.Price >= minprice.Value);
        }

        if (maxprice.HasValue)
        {
            query = query.Where(p => p.Price <= maxprice.Value);
        }
        if(categoryId.HasValue)
        {
            query=query.Where(p=>p.CategoryId== categoryId.Value);
        }
        var products = query.Skip((pagenumber-1)*pagesize).Take(pagesize).Select(
            p => new  ProductResponseDto 
            {
                Id= p.Id,
                Name= p.Name,
                CategoryId= p.CategoryId,
                CategoryName=p.Category.Name,
                Price= p.Price,
            }
            ).ToList();
        return Ok(products);

    }
    [Authorize]
    [HttpGet("{id:guid}")]

    public IActionResult GetProduct([FromRoute] Guid id) {
        var product = _context.Products.Where(p=>p.Id == id).Select(
            
            p=> new ProductResponseDto
            {
                Id=p.Id,
                Name= p.Name,
                CategoryName= p.Category.Name,
                CategoryId= p.Category.Id,
                Price= p.Price,
            }).FirstOrDefault();

        if (product == null)
            return NotFound();

        return Ok(product);


    }
    [Authorize(Roles ="Admin")]
    [HttpPost]

    public IActionResult AddNewProduct([FromBody] ProductRequestDto request)
    {
        var categoryexist = _context.Categories.Any(c => c.Id == request.CategoryId);

        if (!categoryexist)
            return BadRequest("Category Doesnot Exist");
        var product = new Product
        {
            Name= request.Name,
            Price=request.Price,
            CategoryId=request.CategoryId
        };
        _context.Products.Add(product);
        _context.SaveChanges();

        /*
     return CreatedAtAction(
nameof(GetProduct),
new { id = product.Id },
new ProductResponseDto
{
    Id = product.Id,
    Name = product.Name,
    Price = product.Price,
    CategoryId = product.CategoryId,
    CategoryName = _context.Categories
        .Where(c => c.Id == product.CategoryId)
        .Select(c => c.Name)
        .First()
});
     */

        return Created();
    }
    [Authorize(Roles = "Admin")]

    [HttpDelete("{id:guid}")]

    public IActionResult DeleteProduct([FromRoute] Guid id)

    {
        var product = _context.Products.Find(id);
        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        _context.SaveChanges();

        return NoContent();
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]

    public IActionResult UpdateProduct([FromRoute] Guid id, [FromBody] ProductRequestDto request  )

    {
        var product = _context.Products.Find(id);
        if (product == null)
            return NotFound();

        product.Name = request.Name;
        product.CategoryId = request.CategoryId;
        product.Price = request.Price;

        _context.SaveChanges();


        return NoContent();
    }

}