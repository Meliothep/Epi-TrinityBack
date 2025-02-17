using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.DataAccess;
using Inventory.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            List<Product> products = await _context.Products
                                                .Include(i => i.Allergens)
                                                .Include(i => i.Brands)
                                                .Include(i => i.Categories)
                                                .Include(i => i.Origins)
                                                .Include(i => i.Labels)
                                                .ToListAsync();

            return products.ConvertAll(
                new Converter<Product, ProductDTO>(x => x.ToProductDTO()));
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }

            product.Allergens.ForEach(x => _context.Allergens.Find(x.Id));
            product.Brands.ForEach(x => _context.Brands.Find(x.Id));
            product.Categories.ForEach(x => _context.Categories.Find(x.Id));
            product.Labels.ForEach(x => _context.Labels.Find(x.Id));
            product.Origins.ForEach(x => _context.Origins.Find(x.Id));

            return product.ToProductDTO();
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductDTO productRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();

            if (id != productRequest.Id)
            {
                return BadRequest();
            }

            var cs = from c in _context.Categories
                   where productRequest.Categories.Any(s => s.Id == c.Id)
                   select c;
            var bs = from b in _context.Brands
                   where productRequest.Brands.Any(s => s.Id == b.Id)
                   select b;

            Product product = productRequest.ToProduct();
            _context.Products.Attach(product);

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
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();
            
            var p = productRequest.ToProduct();

            p.Allergens.ForEach(x => _context.Attach(x));
            p.Brands.ForEach(x => _context.Attach(x));
            p.Categories.ForEach(x => _context.Attach(x));
            p.Labels.ForEach(x => _context.Attach(x));
            p.Origins.ForEach(x => _context.Attach(x));

            EntityEntry<Product> product = _context.Products.Add(p);
            
            
            await _context.SaveChangesAsync(cancellationToken);

            var result = CreatedAtAction("PostProduct", new { id = product.Entity.Id }, p);

            if(result.GetType() == typeof(ActionResult)){
                return result;
            }

            return ((Product)result.Value).ToProductDTO();
        }

        // DELETE: api/Product/{id}
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
