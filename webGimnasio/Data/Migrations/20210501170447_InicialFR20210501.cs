using Microsoft.EntityFrameworkCore.Migrations;

namespace webGimnasio.Data.Migrations
{
    public partial class InicialFR20210501 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClasesDiarias_AspNetUsers_AlumnoID",
                table: "ClasesDiarias");

            migrationBuilder.DropForeignKey(
                name: "FK_ClasesDiarias_AspNetUsers_ProfesorID",
                table: "ClasesDiarias");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_AspNetUsers_AlumnoID",
                table: "Pagos");

            migrationBuilder.AlterColumn<string>(
                name: "AlumnoID",
                table: "Pagos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfesorID",
                table: "ClasesDiarias",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlumnoID",
                table: "ClasesDiarias",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClasesDiarias_AspNetUsers_AlumnoID",
                table: "ClasesDiarias",
                column: "AlumnoID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ClasesDiarias_AspNetUsers_ProfesorID",
                table: "ClasesDiarias",
                column: "ProfesorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_AspNetUsers_AlumnoID",
                table: "Pagos",
                column: "AlumnoID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClasesDiarias_AspNetUsers_AlumnoID",
                table: "ClasesDiarias");

            migrationBuilder.DropForeignKey(
                name: "FK_ClasesDiarias_AspNetUsers_ProfesorID",
                table: "ClasesDiarias");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_AspNetUsers_AlumnoID",
                table: "Pagos");

            migrationBuilder.AlterColumn<string>(
                name: "AlumnoID",
                table: "Pagos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProfesorID",
                table: "ClasesDiarias",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AlumnoID",
                table: "ClasesDiarias",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ClasesDiarias_AspNetUsers_AlumnoID",
                table: "ClasesDiarias",
                column: "AlumnoID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClasesDiarias_AspNetUsers_ProfesorID",
                table: "ClasesDiarias",
                column: "ProfesorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_AspNetUsers_AlumnoID",
                table: "Pagos",
                column: "AlumnoID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
