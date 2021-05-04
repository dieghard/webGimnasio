using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webGimnasio.Data.Migrations
{
    public partial class UsersRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClasesDiarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfesorID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlumnoID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasesDiarias", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clase");

            migrationBuilder.DropTable(
                name: "ClasesDiarias");
        }
    }
}
