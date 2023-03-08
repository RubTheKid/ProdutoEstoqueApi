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

            return estoque;
        }


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


        [HttpPost]
        public async Task<ActionResult<ItemEstoque>> PostItemEstoque(AddItemEstoqueDto itemEstoque)
        {
            var estoque = new ItemEstoque()
            {
              Nome = itemEstoque.Nome,
                QuantidadeEstoque = itemEstoque.QuantidadeEstoque,
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
