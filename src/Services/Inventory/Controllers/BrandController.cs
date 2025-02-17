using Inventory.DataAccess;
using Inventory.DataAccess.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Trinity.EntityModels.DataAccess;
using Trinity.EntityModels.Models;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public BrandController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Brand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();

            return brands.ConvertAll(
                new Converter<Brand, BrandDTO>(x => x.ToBrandDTO()));
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDTO>> GetBrand(Guid id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand.ToBrandDTO();
        }

        // PUT: api/Brand/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(Guid id, BrandDTO brandRequest)
        {

            CancellationToken cancellationToken = new CancellationToken();

            if (id != brandRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(brandRequest.ToBrand()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brand
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BrandDTO>> PostBrand(BrandDTO brandRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();

            EntityEntry<Brand> b = _context.Brands.Add(brandRequest.ToBrand());
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetBrand", new { id = b.Entity.Id }, b.Entity);
        }

        // DELETE: api/Brand/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            CancellationToken cancellationToken = new CancellationToken();

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool BrandExists(Guid id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
