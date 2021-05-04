using Microsoft.EntityFrameworkCore.Migrations;

namespace webGimnasio.Data.Migrations
{
    public partial class ClasesDiarias_ClaseID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClaseID",
                table: "ClasesDiarias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClasesDiarias_ClaseID",
                table: "ClasesDiarias",
                column: "ClaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClasesDiarias_Clase_ClaseID",
                table: "ClasesDiarias",
                column: "ClaseID",
                principalTable: "Clase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClasesDiarias_Clase_ClaseID",
                table: "ClasesDiarias");

            migrationBuilder.DropIndex(
                name: "IX_ClasesDiarias_ClaseID",
                table: "ClasesDiarias");

            migrationBuilder.DropColumn(
                name: "ClaseID",
                table: "ClasesDiarias");
        }
    }
}
