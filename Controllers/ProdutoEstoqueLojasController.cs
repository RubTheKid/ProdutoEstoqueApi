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
    public class ProdutoEstoqueLojasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoEstoqueLojasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProdutoEstoqueLojas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoEstoqueLoja>>> GetProdutoEstoqueLoja()
        {
            return await _context.ProdutoEstoqueLoja.ToListAsync();
        }

        // GET: api/ProdutoEstoqueLojas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoEstoqueLoja>> GetProdutoEstoqueLoja(int id)
        {
            var produtoEstoqueLoja = await _context.ProdutoEstoqueLoja.FindAsync(id);

            if (produtoEstoqueLoja == null)
            {
                return NotFound();
            }

            return produtoEstoqueLoja;
        }

        // PUT: api/ProdutoEstoqueLojas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutoEstoqueLoja(int id, ProdutoEstoqueLoja produtoEstoqueLoja)
        {
            if (id != produtoEstoqueLoja.Id)
            {
                return BadRequest();
            }

            _context.Entry(produtoEstoqueLoja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoEstoqueLojaExists(id))
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

        // POST: api/ProdutoEstoqueLojas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProdutoEstoqueLoja>> PostProdutoEstoqueLoja(ProdutoEstoqueLoja produtoEstoqueLoja)
        {
            _context.ProdutoEstoqueLoja.Add(produtoEstoqueLoja);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdutoEstoqueLoja", new { id = produtoEstoqueLoja.Id }, produtoEstoqueLoja);
        }

        // DELETE: api/ProdutoEstoqueLojas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdutoEstoqueLoja(int id)
        {
            var produtoEstoqueLoja = await _context.ProdutoEstoqueLoja.FindAsync(id);
            if (produtoEstoqueLoja == null)
            {
                return NotFound();
            }

            _context.ProdutoEstoqueLoja.Remove(produtoEstoqueLoja);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoEstoqueLojaExists(int id)
        {
            return _context.ProdutoEstoqueLoja.Any(e => e.Id == id);
        }
    }
}
