using Microsoft.EntityFrameworkCore;
using ProdutoEstoqueApi.Models;

namespace ProdutoEstoqueApi.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
    { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Loja> Lojas { get; set; }
    public DbSet<ProdutoEstoqueApi.Models.ItemEstoque> ItemEstoque { get; set; }
}
