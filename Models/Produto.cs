using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoEstoqueApi.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(256)]
    public string? Nome { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Preco { get; set; }

    public float? Estoque {get; set; }

    public DateTime DataCadastro  {get; set; }

   
    //Cada produto está mapeada para um estoque
   // public ItemEstoque? ItemEstoque {get; set; }
}
