using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PRODUCTOS_COMPRAS_FULL_STACK.Migrations
{
    /// <inheritdoc />
    public partial class ProductoOrden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Orden_OrdenId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_OrdenId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "OrdenId",
                table: "Producto");

            migrationBuilder.CreateTable(
                name: "ProductoOrden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductoId = table.Column<int>(type: "integer", nullable: false),
                    OrdenId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoOrden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoOrden_Orden_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Orden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoOrden_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoOrden_OrdenId_ProductoId",
                table: "ProductoOrden",
                columns: new[] { "OrdenId", "ProductoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoOrden_ProductoId",
                table: "ProductoOrden",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoOrden");

            migrationBuilder.AddColumn<int>(
                name: "OrdenId",
                table: "Producto",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_OrdenId",
                table: "Producto",
                column: "OrdenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Orden_OrdenId",
                table: "Producto",
                column: "OrdenId",
                principalTable: "Orden",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
