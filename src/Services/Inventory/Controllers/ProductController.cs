using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trinity.EntityModels.DataAccess;
using Trinity.EntityModels.Models;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ProductController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return products.ConvertAll(
                new Converter<Product, ProductResponse>(ProductResponse.MakeProductResponse));
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return ProductResponse.MakeProductResponse(product);
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, UpdateProductRequest productRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();

            if (id != productRequest.Id)
            {
                return BadRequest();
            }

            Category? c = await _context.Categories.FindAsync(productRequest.CategoryId);
            Brand? b = await _context.Brands.FindAsync(productRequest.BrandId);

            Product product = productRequest.MakeProduct(b!, c!);
            

            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> PostProduct(CreateProductRequest productRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();

            Category? c = await _context.Categories.FindAsync(productRequest.CategoryId);
            Brand? b = await _context.Brands.FindAsync(productRequest.BrandId);

            Product product = productRequest.MakeProduct(b!, c!);

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            var result = CreatedAtAction("GetProduct", new { id = product.Id }, product);

            if(result.GetType() == typeof(ActionResult)){
                return result;
            }

            return ProductResponse.MakeProductResponse((Product)result.Value);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            CancellationToken cancellationToken = new CancellationToken();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
