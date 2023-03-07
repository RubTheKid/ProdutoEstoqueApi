using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProdutoEstoqueApi.Models;
using System.Configuration;

namespace ProdutoEstoqueApi.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Loja> Lojas { get; set; }
    public DbSet<ItemEstoque> ItemEstoque { get; set; }

}