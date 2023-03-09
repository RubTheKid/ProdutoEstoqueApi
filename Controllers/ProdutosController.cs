using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using ProdutoEstoqueApi.Context;
using ProdutoEstoqueApi.DTOs;
using ProdutoEstoqueApi.Models;

namespace ProdutoEstoqueApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
        {
            return NotFound(new HttpResult
            {
                Success = false,
                Message = "Produto não encontrado!"
            }); 
        }
        return produto;
    }

    [DisableCors]
    [HttpPut]
    [Route("")]
    public async Task<IActionResult> Put(Produto produto)
    {
        try
        {
            if (!ProdutoExists(produto.ProdutoId))
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

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(AddProdutoDto produto)
    { 
        var prod = new Produto(produto.Nome, produto.Preco);
     
        try
        {
            _context.Produtos.Add(prod);
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
        return CreatedAtAction("getProduto", new {id = prod.ProdutoId}, produto);
    }

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
