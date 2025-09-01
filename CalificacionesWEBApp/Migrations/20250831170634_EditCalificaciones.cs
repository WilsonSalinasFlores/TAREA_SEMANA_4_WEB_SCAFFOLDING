using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalificacionesWEBApp.Migrations
{
    /// <inheritdoc />
    public partial class EditCalificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Calificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfesorId",
                table: "Calificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_CursoId",
                table: "Calificaciones",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_ProfesorId",
                table: "Calificaciones",
                column: "ProfesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Cursos_CursoId",
                table: "Calificaciones",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Profesores_ProfesorId",
                table: "Calificaciones",
                column: "ProfesorId",
                principalTable: "Profesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Cursos_CursoId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Profesores_ProfesorId",
                table: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_CursoId",
                table: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_ProfesorId",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "ProfesorId",
                table: "Calificaciones");
        }
    }
}
