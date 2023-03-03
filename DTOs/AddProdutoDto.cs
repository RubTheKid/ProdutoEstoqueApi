using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProdutoEstoqueApi.DTOs
{
    public class AddProdutoDto
    {
        public string? Nome { get; set; }

        public decimal? Preco { get; set; }

        private DateTime DataCadastro { get; set; }

    }
}
