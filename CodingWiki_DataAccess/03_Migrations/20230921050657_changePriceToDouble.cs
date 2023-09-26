using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changePriceToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Books",
                type: "float(20)",
                precision: 20,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(20)",
                oldPrecision: 20,
                oldScale: 2);
        }
    }
}
