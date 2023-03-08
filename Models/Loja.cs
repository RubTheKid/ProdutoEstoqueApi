using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoEstoqueApi.Models;

[Table("Lojas")]
public class Loja
{
    public Loja()
    { }

    public Loja ( string? nome, string? endereco)
    {
       
        Nome = nome;
        Endereco = endereco;
    }

    [Key]
    public int LojaId { get; set; }

    [StringLength(256)]
    public string? Nome { get; set; }

    [StringLength(512)]
    public string? Endereco { get; set; }

}
