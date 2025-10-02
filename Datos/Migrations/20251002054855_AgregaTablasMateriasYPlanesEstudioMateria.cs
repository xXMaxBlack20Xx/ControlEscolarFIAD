using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class AgregaTablasMateriasYPlanesEstudioMateria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    IdMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaveMateria = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    NombreMateria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HC = table.Column<int>(type: "int", nullable: false),
                    HL = table.Column<int>(type: "int", nullable: false),
                    HT = table.Column<int>(type: "int", nullable: false),
                    HPC = table.Column<int>(type: "int", nullable: false),
                    HCL = table.Column<int>(type: "int", nullable: false),
                    HE = table.Column<int>(type: "int", nullable: false),
                    CR = table.Column<int>(type: "int", nullable: false),
                    PropositoGeneral = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Competencia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Evidencia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Metodologia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Criterios = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BibliografiaBasica = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BibliografiaComplementaria = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PerfilDocente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PathPUA = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EstadoMateria = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.IdMateria);
                });

            migrationBuilder.CreateTable(
                name: "PlanesEstudioMateria",
                columns: table => new
                {
                    IdPlanEstudioMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPlanEstudio = table.Column<int>(type: "int", nullable: false),
                    IdMateria = table.Column<int>(type: "int", nullable: false),
                    Semestre = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesEstudioMateria", x => x.IdPlanEstudioMateria);
                    table.ForeignKey(
                        name: "FK_PlanesEstudioMateria_Materias_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Materias",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanesEstudioMateria_PlanEstudios_IdPlanEstudio",
                        column: x => x.IdPlanEstudio,
                        principalTable: "PlanEstudios",
                        principalColumn: "IdPlanEstudio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanesEstudioMateria_IdMateria",
                table: "PlanesEstudioMateria",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesEstudioMateria_IdPlanEstudio",
                table: "PlanesEstudioMateria",
                column: "IdPlanEstudio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanesEstudioMateria");

            migrationBuilder.DropTable(
                name: "Materias");
        }
    }
}
