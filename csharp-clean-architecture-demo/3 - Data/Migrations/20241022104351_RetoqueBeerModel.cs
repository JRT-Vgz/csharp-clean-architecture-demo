using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3___Data.Migrations
{
    /// <inheritdoc />
    public partial class RetoqueBeerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Alcohol",
                table: "Beer",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Alcohol",
                table: "Beer",
                type: "decimal(2,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
