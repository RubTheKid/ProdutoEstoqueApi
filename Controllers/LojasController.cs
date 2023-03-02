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

namespace ProdutoEstoqueApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LojasController : ControllerBase
{
    private readonly AppDbContext _context;

    public LojasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loja>>> GetLojas()
    {
        return await _context.Lojas.ToListAsync();
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Loja>> GetLoja(int id)
    {
        var loja = await _context.Lojas.FindAsync(id);

        if (loja == null)
        {
            return NotFound("Loja não encontrada!");
        }

        return loja;
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutLoja(int id, Loja loja)
    {
        if (id != loja.LojaId)
        {
            return BadRequest(new HttpResult
            {
                Success = false,
                Message = "Ocorreu um erro. Tente novamente mais tarde."
            });
        }

        try
        {
            if(!LojaExists(id))
            {
                return NotFound(new HttpResult
                {
                    Success = false,
                    Message = "Loja não Encontrada"
                });
            }
            else
            {
                _context.Lojas.Update(loja);
                await _context.SaveChangesAsync();
            }

        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!LojaExists(id))
            {
                return BadRequest(new HttpResult
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            return NoContent();
            }
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Loja>> PostLoja(Loja loja)
    {
        _context.Lojas.Add(loja);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLoja", new { id = loja.LojaId }, loja);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLoja(int id)
    {
        var loja = await _context.Lojas.FindAsync(id);

        if (loja == null)
        {
            return NotFound();
        }
        try
        {
            _context.Lojas.Remove(loja);
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

        private bool LojaExists(int id)
        {
            return _context.Lojas.Any(e => e.LojaId == id);
        }
}
