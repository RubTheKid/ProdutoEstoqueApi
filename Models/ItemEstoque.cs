using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoEstoqueApi.Models;


[Table("ItemEstoques")]
public class ItemEstoque
{
    //inicialização da coleção produtos e lojas
    public ItemEstoque()
    {
        Produtos = new Collection<Produto>();
        Lojas = new Collection<Loja>();
    }

    [Key]
    public int ItemEstoqueId { get; set; }

    [StringLength(128)]
    public string? Nome { get; set; }
    

    //cada ItemEstoque pode ter uma coleção de produtos
    public ICollection<Produto>? Produtos { get; set; }
    //cada ItemEstoque pode ter uma coleção de lojas
    public ICollection<Loja>? Lojas { get; set; }
}
/*
 * Tomei a liberdade de alterar a cardinalidade do relacionamento entre as entidades, pois me fez mais sentido
 * uma loja ter 1 ou mais estoques, e um estoque ter N produtos.
 */