﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutoEstoqueApi.Models;


[Table("ItemEstoques")]
public class ItemEstoque
{
    [Key]
    public int ItemEstoqueId { get; set; }

    [StringLength(128)]
    public string? Nome { get; set; }

    public Produto? Produto { get; set; }

    public Loja? Loja { get; set; }
}
