using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRODUCTOS_COMPRAS_FULL_STACK.Migrations
{
    /// <inheritdoc />
    public partial class Tabla_Orden_Sin_Producto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Producto_ProductoId",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Compra_OrdenId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Compra_ProductoId",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Compra");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Compra_OrdenId",
                table: "Producto",
                column: "OrdenId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Compra_OrdenId",
                table: "Producto");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Compra",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compra_ProductoId",
                table: "Compra",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Producto_ProductoId",
                table: "Compra",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Compra_OrdenId",
                table: "Producto",
                column: "OrdenId",
                principalTable: "Compra",
                principalColumn: "Id");
        }
    }
}
