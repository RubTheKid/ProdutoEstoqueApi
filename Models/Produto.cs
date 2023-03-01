using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoEstoqueApi.Models;

[Table("Produtos")]
public class Produto
{
    public Produto()
    { }

    public Produto( string? nome, decimal? preco)
    {
        Nome = nome;
        Preco = preco;
        DataCadastro = DateTime.Now;
    }

    [Key]
    public int ProdutoId { get; set; }

    [StringLength(256)]
    public string? Nome { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Preco { get; set; }

    public DateTime DataCadastro  {get; set; }

    //[InverseProperty("Produto")]
    //public ICollection<ItemEstoque>? ItemEstoques { get; set; }

 

}
