using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRODUCTOS_COMPRAS_FULL_STACK.Migrations
{
    /// <inheritdoc />
    public partial class Tabla_Orden_Sin_Precio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Compra_OrdenId",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compra",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "precio",
                table: "Compra");

            migrationBuilder.RenameTable(
                name: "Compra",
                newName: "Orden");

            migrationBuilder.AlterColumn<int>(
                name: "OrdenId",
                table: "Producto",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orden",
                table: "Orden",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Orden_OrdenId",
                table: "Producto",
                column: "OrdenId",
                principalTable: "Orden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Orden_OrdenId",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orden",
                table: "Orden");

            migrationBuilder.RenameTable(
                name: "Orden",
                newName: "Compra");

            migrationBuilder.AlterColumn<int>(
                name: "OrdenId",
                table: "Producto",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddColumn<float>(
                name: "precio",
                table: "Compra",
                type: "FLOAT",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compra",
                table: "Compra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Compra_OrdenId",
                table: "Producto",
                column: "OrdenId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
