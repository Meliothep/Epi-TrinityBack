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
    public class OriginController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public OriginController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Origin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OriginDTO>>> GetOrigins()
        {
            List<Origin> origins = await _context.Origins.ToListAsync();

            return origins.ConvertAll(
                new Converter<Origin, OriginDTO>(x => x.ToOriginDTO()));
        }

        // GET: api/Origin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OriginDTO>> GetOrigin(Guid id)
        {
            var origin = await _context.Origins.FindAsync(id);

            if (origin == null)
            {
                return NotFound();
            }

            return origin.ToOriginDTO();
        }

        // PUT: api/Origin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrigin(Guid id, OriginDTO originRequest)
        {

            CancellationToken cancellationToken = new CancellationToken();

            if (id != originRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(originRequest.ToOrigin()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OriginExists(id))
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

        // POST: api/Origin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OriginDTO>> PostOrigin(OriginDTO originRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();

            EntityEntry<Origin> b = _context.Origins.Add(originRequest.ToOrigin());
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetOrigin", new { id = b.Entity.Id }, b.Entity);
        }

        // DELETE: api/Origin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrigin(Guid id)
        {
            CancellationToken cancellationToken = new CancellationToken();

            var origin = await _context.Origins.FindAsync(id);
            if (origin == null)
            {
                return NotFound();
            }

            _context.Origins.Remove(origin);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool OriginExists(Guid id)
        {
            return _context.Origins.Any(e => e.Id == id);
        }
    }
}
