using InventoryMS_CRUD_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS_CRUD_APIs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {

        private readonly ApplicationDBContext _context;

        // Constructor to inject ApplicationDBContext dependency
        public ProductController(ApplicationDBContext context)
        {
            _context = context;
        }



        /*----- CREATE -----*/
        //create new product API
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        /*----- READ -----*/
        //get all products API
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _context.Products
                    .ToList();
                return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }

        }

        /*------GET PRODUCT BY ID------*/
        //get product by id API
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /*----- UPDATE -----*/
        //edit product API
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                _context.Products.Update(product);
                _context.SaveChanges(true);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /*----- DELETE -----*/
        //delete product API
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {

            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                //set the soft delete flag to true
                product.IsDeleted = true;
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
