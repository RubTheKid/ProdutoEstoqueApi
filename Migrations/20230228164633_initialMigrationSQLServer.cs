using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdutoEstoqueApi.Migrations
{
    /// <inheritdoc />
    public partial class initialMigrationSQLServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemEstoques",
                columns: table => new
                {
                    ItemEstoqueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEstoques", x => x.ItemEstoqueId);
                });

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    LojaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ItemEstoqueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.LojaId);
                    table.ForeignKey(
                        name: "FK_Lojas_ItemEstoques_ItemEstoqueId",
                        column: x => x.ItemEstoqueId,
                        principalTable: "ItemEstoques",
                        principalColumn: "ItemEstoqueId");
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Estoque = table.Column<float>(type: "real", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemEstoqueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produtos_ItemEstoques_ItemEstoqueId",
                        column: x => x.ItemEstoqueId,
                        principalTable: "ItemEstoques",
                        principalColumn: "ItemEstoqueId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lojas_ItemEstoqueId",
                table: "Lojas",
                column: "ItemEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ItemEstoqueId",
                table: "Produtos",
                column: "ItemEstoqueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "ItemEstoques");
        }
    }
}
