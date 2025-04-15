using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRODUCTOS_COMPRAS_FULL_STACK.Migrations
{
    /// <inheritdoc />
    public partial class Tabla_Orden_Total_Float_Columna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "total",
                table: "Orden",
                type: "FLOAT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "total",
                table: "Orden",
                type: "INT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "FLOAT");
        }
    }
}
