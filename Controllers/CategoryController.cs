using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagment.Data;
using StoreManagment.DTO;
using StoreManagment.Models;

namespace StoreManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]

    public IActionResult GetAllCategories( [FromQuery] string?name, [FromQuery] int pagenumber = 1, [FromQuery] int pagesize = 10)
    {
        if (pagenumber < 1 || pagesize < 1 || pagesize > 50)
            return BadRequest("Invalid page number ");

        var query = _context.Categories.OrderBy(p => p.Name).AsQueryable();


        if(!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        var category = query.Skip((pagenumber-1)*pagesize).Take(pagesize).Select(
            p => new CategoryResponseDto
            {
                Name = p.Name,
                Id = p.Id,
            }

            ).ToList();

        return Ok(category);
    }



    [HttpGet("{id:guid}")]

    public IActionResult GetCategory([FromRoute] Guid id)
    {
        var category = _context.Categories.Where(p => p.Id == id).Select(

            p => new CategoryResponseDto
            {
                Id = p.Id,
                Name = p.Name,

            }).FirstOrDefault();

        if (category == null)
            return NotFound();

        return Ok(category);


    }

    [HttpPost]
    public IActionResult AddNewCategory([FromBody] CategoryRequestDto request)
    {
        var newcategory = new Category { Name = request.Name };

        _context.Categories.Add(newcategory);
        _context.SaveChanges();
        var response = new CategoryResponseDto
        {
            Id = newcategory.Id,
            Name = newcategory.Name
        };

        return CreatedAtAction(
            nameof(GetCategory),
            new { id = newcategory.Id },
            response);

        // return Created();

    }


    [HttpDelete("{id:guid}")]

    public IActionResult DeleteCategory([FromRoute] Guid id)

    {
        var category = _context.Categories.Find(id);
        if (category == null)
            return NotFound();

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return NoContent();
    }


    [HttpPut("{id:guid}")]

    public IActionResult UpdateCategory([FromRoute] Guid id, [FromBody] CategoryRequestDto request)

    {
        var category = _context.Categories.Find(id);
        if (category == null)
            return NotFound();

        category.Name = request.Name;


        _context.SaveChanges();


        return NoContent();
    }



}
