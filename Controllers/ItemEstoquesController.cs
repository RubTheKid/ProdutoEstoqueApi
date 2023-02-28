using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutoEstoqueApi.Context;
using ProdutoEstoqueApi.Models;

namespace ProdutoEstoqueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemEstoquesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemEstoquesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemEstoques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemEstoque>>> GetItemEstoque()
        {
            return await _context.ItemEstoque.ToListAsync();
        }

        // GET: api/ItemEstoques/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemEstoque>> GetItemEstoque(int id)
        {
            var itemEstoque = await _context.ItemEstoque.FindAsync(id);

            if (itemEstoque == null)
            {
                return NotFound();
            }

            return itemEstoque;
        }

        // PUT: api/ItemEstoques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutItemEstoque(int id, ItemEstoque itemEstoque)
        {
            if (id != itemEstoque.ItemEstoqueId)
            {
                return BadRequest();
            }

            _context.Entry(itemEstoque).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemEstoqueExists(id))
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

        // POST: api/ItemEstoques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemEstoque>> PostItemEstoque(ItemEstoque itemEstoque)
        {
            _context.ItemEstoque.Add(itemEstoque);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemEstoque", new { id = itemEstoque.ItemEstoqueId }, itemEstoque);
        }

        // DELETE: api/ItemEstoques/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItemEstoque(int id)
        {
            var itemEstoque = await _context.ItemEstoque.FindAsync(id);
            if (itemEstoque == null)
            {
                return NotFound();
            }

            _context.ItemEstoque.Remove(itemEstoque);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemEstoqueExists(int id)
        {
            return _context.ItemEstoque.Any(e => e.ItemEstoqueId == id);
        }
    }
}
