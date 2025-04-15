using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PRODUCTOS_COMPRAS_FULL_STACK.Migrations
{
    /// <inheritdoc />
    public partial class Tabla_Producto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    precio = table.Column<float>(type: "FLOAT", nullable: false),
                    total = table.Column<int>(type: "INT", nullable: false),
                    ProductoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Precio = table.Column<float>(type: "FLOAT", nullable: false),
                    stock = table.Column<int>(type: "INT", nullable: false),
                    compraId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Compra_compraId",
                        column: x => x.compraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_ProductoId",
                table: "Compra",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_compraId",
                table: "Producto",
                column: "compraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Producto_ProductoId",
                table: "Compra",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Producto_ProductoId",
                table: "Compra");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Compra");
        }
    }
}
