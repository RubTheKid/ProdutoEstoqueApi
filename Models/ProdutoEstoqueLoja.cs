using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoEstoqueApi.Models
{
    [Table("ProdutoEstoqueLoja")]
    public class ProdutoEstoqueLoja
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int LojaId { get; set; }
        public int ItemEstoqueId { get; set; }

        [ForeignKey("ProdutoId")]
        public Produto? Produto { get; set; }

        [ForeignKey("LojaId")]
        public Loja? Loja { get; set; }

        [ForeignKey("ItemEstoqueId")]
        public ItemEstoque? ItemEstoque { get; set; }
        
    }
}
