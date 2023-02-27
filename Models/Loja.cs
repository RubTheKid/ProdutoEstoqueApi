using System.ComponentModel.DataAnnotations;

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

    //Cada loja está mapeada para um estoque
    public ItemEstoque? ItemEstoque { get; set; }

}
