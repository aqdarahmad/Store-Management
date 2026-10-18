using Microsoft.AspNetCore.Mvc;
using StoreManagment.Data;
using StoreManagment.Models;
using StoreManagment.DTO;

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

    [HttpGet]

    public IActionResult GetAllProduct()
    {
        var products = _context.Products.Select(
            p => new  ProductResponseDto 
            {
                Id= p.Id,
                Name= p.Name,
                CategoryId= p.CategoryId,
                CategoryName=p.Category.Name
            }
            ).ToList();
        return Ok(products);

    }

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