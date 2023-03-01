using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoEstoqueApi.Models;

[Table("Produtos")]
public class Produto
{
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
