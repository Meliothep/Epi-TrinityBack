using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Trinity.EntityModels.DataAccess;
using Trinity.EntityModels.Models;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public CategoryController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            List<Category> categorys = await _context.Categories.ToListAsync();

            return categorys.ConvertAll(
                new Converter<Category, CategoryDTO>(CategoryDTO.MakeDTO));
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return CategoryDTO.MakeDTO(category);
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, CategoryDTO categoryRequest)
        {

            CancellationToken cancellationToken = new CancellationToken();

            if (id != categoryRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(CategoryDTO.MakeModel(categoryRequest)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryRequest)
        {
            CancellationToken cancellationToken = new CancellationToken();

            EntityEntry<Category> b = _context.Categories.Add(CategoryDTO.MakeModel(categoryRequest));
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetCategory", new { id = b.Entity.Id }, b.Entity);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            CancellationToken cancellationToken = new CancellationToken();

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
