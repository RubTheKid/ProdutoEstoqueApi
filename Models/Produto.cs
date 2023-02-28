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

    public DateTime DataCadastro  {get; set; }

    [InverseProperty("Produto")]
    public ICollection<ProdutoEstoqueLoja>? ProdutoEstoqueLojas { get; set; }

}
