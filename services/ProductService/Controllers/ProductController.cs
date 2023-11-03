using BTH.H23.PA2577.Assignment1.ProductService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTH.H23.PA2577.Assignment1.ProductService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public ProductController(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts() => await _databaseContext.Products.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var product = await _databaseContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            var entity = _databaseContext.Add(product);
            await _databaseContext.SaveChangesAsync();

            return Ok(entity.Entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(Guid id, [FromBody] Product newProduct)
        {
            if (id != newProduct.Id)
            {
                return BadRequest();
            }

            DbSet<Product> productSet = _databaseContext.Products;

            var product = productSet.First(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.Author = newProduct.Author;
            product.Publisher = newProduct.Publisher;
            product.Description = newProduct.Description;

            var entry = _databaseContext.Update(product);
            await _databaseContext.SaveChangesAsync(true);
            return Ok(entry.Entity);
        }

        // DELETE: /Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _databaseContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _databaseContext.Products.Remove(product);
            await _databaseContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("inventory")]
        public async Task<ActionResult<IEnumerable<ProductInventory>>> GetProductsInventory([FromBody] Guid[] productIds)
        {
            if (productIds == null || productIds.Length == 0)
            {
                return BadRequest("Product IDs cannot be null or empty.");
            }

            try
            {
                // Assuming you have a Quantity property on your Product model.
                var productInventories = await _databaseContext.Products
                    .Where(p => productIds.Contains(p.Id))
                    .Select(p => new ProductInventory
                    {
                        ProductId = p.Id,
                        QuantityAvailable = p.Quantity // Replace with your actual quantity property
                    })
                    .ToListAsync();

                // Check if all requested product IDs were found
                var foundProductIds = productInventories.Select(pi => pi.ProductId).ToHashSet();
                var notFoundProductIds = productIds.Where(id => !foundProductIds.Contains(id)).ToList();
                if (notFoundProductIds.Any())
                {
                    // You could also return a specific DTO with ProductId and a NotFound status, for example.
                    return NotFound($"No inventory found for product IDs: {string.Join(", ", notFoundProductIds)}");
                }

                return productInventories;
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the products inventory.");
            }
        }
    }

    // DTO class to represent the inventory data
    public class ProductInventory
    {
        public Guid ProductId { get; set; }
        public int QuantityAvailable { get; set; }
    }
}