using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRODUCTOS_COMPRAS_FULL_STACK.Migrations
{
    /// <inheritdoc />
    public partial class Tabla_Orden_Corregida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Compra_compraId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_compraId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "compraId",
                table: "Producto");

            migrationBuilder.AddColumn<int>(
                name: "OrdenId",
                table: "Producto",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_OrdenId",
                table: "Producto",
                column: "OrdenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Compra_OrdenId",
                table: "Producto",
                column: "OrdenId",
                principalTable: "Compra",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Compra_OrdenId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_OrdenId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "OrdenId",
                table: "Producto");

            migrationBuilder.AddColumn<int>(
                name: "compraId",
                table: "Producto",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_compraId",
                table: "Producto",
                column: "compraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Compra_compraId",
                table: "Producto",
                column: "compraId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
