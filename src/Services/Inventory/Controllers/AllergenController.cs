using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Trinity.EntityModels.DataAccess;
using Trinity.EntityModels.Models;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergenController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public AllergenController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Allergen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllergenDTO>>> GetAllergens()
        {
            List<Allergen> allergens = await _context.Allergens.ToListAsync();

            return allergens.ConvertAll(
                new Converter<Allergen, AllergenDTO>(AllergenDTO.MakeDTO));
        }

        // GET: api/Allergen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllergenDTO>> GetAllergen(Guid id)
        {
            var allergen = await _context.Allergens.FindAsync(id);

            if (allergen == null)
            {
                return NotFound();
            }

            return AllergenDTO.MakeDTO(allergen);
        }

        // PUT: api/Allergen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAllergen(Guid id, AllergenDTO allergenRequest)
        {

            CancellationToken cancellationToken = new CancellationToken();

            if (id != allergenRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(AllergenDTO.MakeModel(allergenRequest)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllergenExists(id))
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

        // POST: api/Allergen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AllergenDTO>> PostAllergen(AllergenDTO allergenRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();

            EntityEntry<Allergen> b = _context.Allergens.Add(AllergenDTO.MakeModel(allergenRequest));
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetAllergen", new { id = b.Entity.Id }, b.Entity);
        }

        // DELETE: api/Allergen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllergen(Guid id)
        {
            CancellationToken cancellationToken = new CancellationToken();

            var allergen = await _context.Allergens.FindAsync(id);
            if (allergen == null)
            {
                return NotFound();
            }

            _context.Allergens.Remove(allergen);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool AllergenExists(Guid id)
        {
            return _context.Allergens.Any(e => e.Id == id);
        }
    }
}
