using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class EliminarRestriccionUnicaClaveCarrera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UK_Carreras_Clave",
                schema: "CEF",
                table: "Carreras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UK_Carreras_Clave",
                schema: "CEF",
                table: "Carreras",
                column: "ClaveCarrera",
                unique: true);
        }
    }
}
