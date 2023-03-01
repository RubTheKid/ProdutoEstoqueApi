using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdutoEstoqueApi.Migrations
{
    /// <inheritdoc />
    public partial class _6thmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemEstoques_Lojas_LojaId",
                table: "ItemEstoques");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEstoques_Produtos_ProdutoId",
                table: "ItemEstoques");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItemEstoques",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LojaId",
                table: "ItemEstoques",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEstoques_Lojas_LojaId",
                table: "ItemEstoques",
                column: "LojaId",
                principalTable: "Lojas",
                principalColumn: "LojaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEstoques_Produtos_ProdutoId",
                table: "ItemEstoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItemEstoques",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LojaId",
                table: "ItemEstoques",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
