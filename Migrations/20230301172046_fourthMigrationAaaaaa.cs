using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdutoEstoqueApi.Migrations
{
    /// <inheritdoc />
    public partial class fourthMigrationAaaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoEstoqueLoja");

            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "ItemEstoques",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "ItemEstoques",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemEstoques_LojaId",
                table: "ItemEstoques",
                column: "LojaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEstoques_ProdutoId",
                table: "ItemEstoques",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEstoques_Lojas_LojaId",
                table: "ItemEstoques",
                column: "LojaId",
                principalTable: "Lojas",
                principalColumn: "LojaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEstoques_Produtos_ProdutoId",
                table: "ItemEstoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemEstoques_Lojas_LojaId",
                table: "ItemEstoques");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEstoques_Produtos_ProdutoId",
                table: "ItemEstoques");

            migrationBuilder.DropIndex(
                name: "IX_ItemEstoques_LojaId",
                table: "ItemEstoques");

            migrationBuilder.DropIndex(
                name: "IX_ItemEstoques_ProdutoId",
                table: "ItemEstoques");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "ItemEstoques");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "ItemEstoques");

            migrationBuilder.CreateTable(
                name: "ProdutoEstoqueLoja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemEstoqueId = table.Column<int>(type: "int", nullable: false),
                    LojaId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoEstoqueLoja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoEstoqueLoja_ItemEstoques_ItemEstoqueId",
                        column: x => x.ItemEstoqueId,
                        principalTable: "ItemEstoques",
                        principalColumn: "ItemEstoqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoEstoqueLoja_Lojas_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Lojas",
                        principalColumn: "LojaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoEstoqueLoja_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoEstoqueLoja_ItemEstoqueId",
                table: "ProdutoEstoqueLoja",
                column: "ItemEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoEstoqueLoja_LojaId",
                table: "ProdutoEstoqueLoja",
                column: "LojaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoEstoqueLoja_ProdutoId",
                table: "ProdutoEstoqueLoja",
                column: "ProdutoId");
        }
    }
}
