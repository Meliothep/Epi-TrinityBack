using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Trinity.EntityModels.DataAccess;
using Trinity.EntityModels.Models;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public LabelController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Label
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabelDTO>>> GetLabels()
        {
            List<Label> labels = await _context.Labels.ToListAsync();

            return labels.ConvertAll(
                new Converter<Label, LabelDTO>(LabelDTO.MakeDTO));
        }

        // GET: api/Label/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LabelDTO>> GetLabel(Guid id)
        {
            var label = await _context.Labels.FindAsync(id);

            if (label == null)
            {
                return NotFound();
            }

            return LabelDTO.MakeDTO(label);
        }

        // PUT: api/Label/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLabel(Guid id, LabelDTO labelRequest)
        {

            CancellationToken cancellationToken = new CancellationToken();

            if (id != labelRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(LabelDTO.MakeModel(labelRequest)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabelExists(id))
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

        // POST: api/Label
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LabelDTO>> PostLabel(LabelDTO labelRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();

            EntityEntry<Label> b = _context.Labels.Add(LabelDTO.MakeModel(labelRequest));
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetLabel", new { id = b.Entity.Id }, b.Entity);
        }

        // DELETE: api/Label/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabel(Guid id)
        {
            CancellationToken cancellationToken = new CancellationToken();

            var label = await _context.Labels.FindAsync(id);
            if (label == null)
            {
                return NotFound();
            }

            _context.Labels.Remove(label);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool LabelExists(Guid id)
        {
            return _context.Labels.Any(e => e.Id == id);
        }
    }
}
