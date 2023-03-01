using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutoEstoqueApi.Context;
using ProdutoEstoqueApi.DTOs;
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
        public async Task<List<ItemEstoque>> GetItemEstoque()
        {
            var estoque = await _context.ItemEstoque
                .Select(ie => new ItemEstoque()
                {
                    ItemEstoqueId = ie.ItemEstoqueId,
                    Nome = ie.Nome,
                    Produto = _context.Produtos
                        .Select(p => p)
                        .Where(p => p == ie.Produto)
                        .FirstOrDefault(),
                    Loja = _context.Lojas
                        .Select(l => l)
                        .Where(l => l == ie.Loja)
                        .FirstOrDefault()
                }).ToListAsync();


            /*var estoque = await _context.ItemEstoque
                .Select(ie => ie)
                .Where(ie => ie.ProdutoId == itemEstoque.ProdutoId && ie.LojaId == itemEstoque.LojaId)
                .Select(itemEstoque => new ItemEstoque()
                {
                    Nome = itemEstoque.Nome,
                    Produto = _context.Produtos
                        .Select(p => p)
                        .Where(p => p.ProdutoId == itemEstoque.ProdutoId)
                        .FirstOrDefault(),
                    Loja = _context.Lojas
                        .Select(l => l)
                        .Where(l => l.LojaId == itemEstoque.LojaId)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();*/

            //if (estoque == null)
            //{
            //    return NotFound();
            //}

            return estoque;
        }

        // GET: api/ItemEstoques/5
        [HttpGet("{id:int}")]
        public async Task<ItemEstoque> GetItemEstoque(int id)
        {
            var estoque = await _context.ItemEstoque
                .Select(ie => new ItemEstoque()
                {
                    ItemEstoqueId = ie.ItemEstoqueId,
                    Nome = ie.Nome,
                    Produto = _context.Produtos
                        .Select(p => p)
                        .Where(p => p == ie.Produto)
                        .FirstOrDefault(),
                    Loja = _context.Lojas
                        .Select(l => l)
                        .Where(l => l == ie.Loja)
                        .FirstOrDefault()

                })
                .Where(ie => ie.ItemEstoqueId == id)
                .FirstOrDefaultAsync();
     
            return estoque;
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
        public async Task<ActionResult<ItemEstoque>> PostItemEstoque(AddItemEstoqueDto itemEstoque)
        {
            var estoque = new ItemEstoque()
            {
              Nome = itemEstoque.Nome,
              Produto = _context.Produtos
                        .Select(produto => produto)
                        .Where(produto => produto.ProdutoId == itemEstoque.ProdutoId)
                        .FirstOrDefault(),
              Loja = _context.Lojas
                        .Select(loja => loja)
                        .Where(loja => loja.LojaId == itemEstoque.LojaId).FirstOrDefault()
            };
            _context.ItemEstoque.Add(estoque);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemEstoque", new { id = estoque.ItemEstoqueId }, estoque);
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
