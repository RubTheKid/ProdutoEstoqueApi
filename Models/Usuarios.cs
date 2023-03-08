using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace ProdutoEstoqueApi.Models;

[Table("Usuarios")]
public class Usuarios
{

    [StringLength(50)]
    [Required]
    [NotNull]
    public string Username { get; set; }

    [StringLength(50)]
    [Required]
    [NotNull]
    public string Password { get; set; }
}
