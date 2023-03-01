using ProdutoEstoqueApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProdutoEstoqueApi.DTOs
{
    public class AddItemEstoqueDto
    {
        [Required]
        private int ItemEstoqueId { get; set; }

        public string? Nome { get; set; }

        public int ProdutoId { get; set; }

        public int LojaId { get; set; }
    }
}
