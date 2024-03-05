using InventoryMS_CRUD_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS_CRUD_APIs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _context;
        public CategoryController(ApplicationDBContext context)
        {
            _context = context;
        }

        //add new category API
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
                
        }

        //get all categories API
        [HttpGet]
        public IActionResult GetCategories()
        {
           
         
            try
            {
               var categories = _context.Categories
                                              .Include(c => c.Products) // Include products
                                              .ToList();
               // var categories = _context.Categories.ToList();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
            
        }

        //edit category API
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                _context.Categories.Update(category);
                _context.SaveChanges(true);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        //delete category API
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = _context.Categories.Find(id);
                if (category == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                _context.Categories.Remove(category);
                _context.SaveChanges(true);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



    }
}
