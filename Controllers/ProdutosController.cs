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
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        // GET: api/Produtos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            
            if (produto == null)
            {
                return NotFound("Produto não encontrado!");
            }

            return produto;
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Ocorreu um erro. Tente novamente mais tarde.");
            }

            try
            {
                if (!ProdutoExists(id))
                {
                    return NotFound(new HttpResult
                    {
                        Success = false,
                        Message = "Produto não encontrado."
                    });
                }
                else
                {
                    _context.Produtos.Update(produto);

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new HttpResult
                {
                    Success = false,
                    Message = ex.Message
                });
            }

            return NoContent();
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(AddProdutoDto produto)
        { 
            var p = new Produto(produto.Nome, produto.Preco);
         
            try
            {
                _context.Produtos.Add(p);
                await _context.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                return BadRequest(new HttpResult
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            return CreatedAtAction("getProduto", new {id = p.ProdutoId});
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            try
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpResult
                {
                    Success = false,
                    Message = ex.Message
                });
            }

            return NoContent();
 
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
