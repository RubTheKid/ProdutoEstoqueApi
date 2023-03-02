using System.ComponentModel.DataAnnotations;

namespace ProdutoEstoqueApi.DTOs
{
    public class AddLojaDto
    {
        public string? Nome { get; set; }

        public string? Endereco { get; set; }
    }
}
