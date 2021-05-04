using Microsoft.EntityFrameworkCore.Migrations;

namespace webGimnasio.Data.Migrations
{
    public partial class PagoImporte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Importe",
                table: "Pagos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Importe",
                table: "Pagos");
        }
    }
}
