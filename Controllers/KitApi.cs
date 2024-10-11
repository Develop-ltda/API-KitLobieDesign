using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KitLobieDesign.Context;
using KitLobieDesign.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitLobieDesign.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KitController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KitController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kit>>> GetKits()
        {
            return await _context.Kits
                .AsNoTracking()
                .Include(k => k.Categories)
                .ThenInclude(c => c.Items)
                .AsSplitQuery()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kit>> GetKit(int id)
        {
            var kit = await _context.Kits.Include(k => k.Categories)
                                     .ThenInclude(c => c.Items)
                                     .FirstOrDefaultAsync(k => k.Id == id);

            if (kit == null)
            {
                return NotFound();
            }
            return kit;
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpGet("category/{categoryId}/items")]
        public async Task<ActionResult<Item>> GetItem(string categoryId, int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }


      
        [HttpPost]
        public async Task<ActionResult<Kit>> CreateKit([FromBody] Kit kit) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try 
            {
                 _context.Kits.Add(kit);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetKit), new { id = kit.Id }, kit);
            } 
            catch (DbUpdateException ) 
            {
        
            return StatusCode(500, "Database error occurred");
            } 
            catch (Exception)
            {
        
                return StatusCode(500, "An unexpected error occurred");
            }
        }


          [HttpPost("kit/{kitId}/category")]
          public async Task<ActionResult<Category>> CreateCategory(int kitId, Category category)
          {
              var kit = await _context.Kits.FindAsync(kitId);
              if (kit == null)
              {
                  return NotFound("Kit não encontrado");
              }

              category.KitId = kitId;
              _context.Categories.Add(category);
              await _context.SaveChangesAsync();

              return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
          }

        [HttpPost("category/{categoryId}/items")]
        public async Task<ActionResult<Item>> AddItemToCategory(int categoryId, [FromBody] Item item)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound("Categoria não encontrada");
            }

    
            item.CategoryId = categoryId;
            _context.Items.Add(item);

   
            category.TotalPrice = category.Items.Sum(i => i.Price) + item.Price;

   
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateKit(int id, Kit updatedKit)
        {
            var kit = await _context.Kits.FindAsync(id);
            if (kit == null)
            {
                return NotFound("Kit não encontrado");
            }

  
            kit.Name = updatedKit.Name ?? kit.Name;
            kit.Price = updatedKit.Price > 0 ? updatedKit.Price : kit.Price;
            kit.Description = updatedKit.Description ?? kit.Description;

            _context.Entry(kit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("category/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory)
        {
            if (id != updatedCategory.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(updatedCategory).State = EntityState.Modified;

            foreach (var item in updatedCategory.Items)
            {
                if (item.Id == 0)
                {
                    _context.Items.Add(item);
                }
                else
                {
                    _context.Entry(item).State = EntityState.Modified;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
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
        [HttpPatch("category/{categoryId}/items/{itemId}")]
        public async Task<IActionResult> UpdateItem(int categoryId, int itemId, [FromBody] Item updatedItem)
        {
            if (itemId != updatedItem.Id)
            {
            return BadRequest();
            }

            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound("Categoria não encontrada");
            }

            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
            {
                return NotFound("Item não encontrado");
            }

    
          category.TotalPrice = category.Items.Sum(i => i.Price) - item.Price + updatedItem.Price;

    
        item.Name = updatedItem.Name;
        item.Price = updatedItem.Price;
        _context.Entry(item).State = EntityState.Modified;
        _context.Entry(category).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ItemExists(int id)
    {
        return _context.Items.Any(e => e.Id == id);
    }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKit(int id)
        {
            var kit = await _context.Kits
            .Include(k => k.Categories)
            .ThenInclude(c => c.Items)
            .FirstOrDefaultAsync(k => k.Id == id);

            if (kit == null)
            {
            return NotFound();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                    foreach (var category in kit.Categories)
                {
                    _context.Items.RemoveRange(category.Items);
                }

                _context.Categories.RemoveRange(kit.Categories);
                _context.Kits.Remove(kit);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(kit);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Erro ao deletar o kit: {ex.Message}");
            }
        }


        [HttpDelete("category/{categoryId}/items/{itemId}")]
        public async Task<IActionResult> DeleteItemFromCategory(int categoryId, int itemId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound("Categoria não encontrada");
            }

            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId && i.CategoryId == categoryId);
            if (item == null)
            {
                return NotFound("Item não encontrado");
            }

   
            _context.Items.Remove(item);

   
            category.TotalPrice = category.Items.Sum(i => i.Price) - item.Price;

    
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }
    }
} 

