using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoEstoqueApi.Models;

public class Loja
{
    [Key]
    public int LojaId { get; set; }

    [Required]
    [StringLength(256)]
    public string? Nome { get; set; }

    [StringLength(512)]
    public string? Endereco { get; set; }

    [InverseProperty("Loja")]
    public ICollection<ProdutoEstoqueLoja>? ProdutoEstoqueLojas { get; set; }

}
