using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class AgregamosNivelesAcademicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NivelesAcademicos",
                columns: table => new
                {
                    IdNivelAcademico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreNivelAcademico = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesAcademicos", x => x.IdNivelAcademico);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanEstudios_IdNivelAcademico",
                table: "PlanEstudios",
                column: "IdNivelAcademico");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanEstudios_NivelesAcademicos_IdNivelAcademico",
                table: "PlanEstudios",
                column: "IdNivelAcademico",
                principalTable: "NivelesAcademicos",
                principalColumn: "IdNivelAcademico",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanEstudios_NivelesAcademicos_IdNivelAcademico",
                table: "PlanEstudios");

            migrationBuilder.DropTable(
                name: "NivelesAcademicos");

            migrationBuilder.DropIndex(
                name: "IX_PlanEstudios_IdNivelAcademico",
                table: "PlanEstudios");
        }
    }
}
