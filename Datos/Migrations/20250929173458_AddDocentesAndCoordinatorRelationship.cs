using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class AddDocentesAndCoordinatorRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanesEstudio_Carreras_IdCarrera",
                schema: "CEF",
                table: "PlanesEstudio");

            migrationBuilder.DropIndex(
                name: "UK_Carreras_Alias",
                schema: "CEF",
                table: "Carreras");

            migrationBuilder.DropIndex(
                name: "UK_Carreras_Nombre",
                schema: "CEF",
                table: "Carreras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanesEstudio",
                schema: "CEF",
                table: "PlanesEstudio");

            migrationBuilder.DropIndex(
                name: "UK_PlanesEstudio_IdCarrera_Plan",
                schema: "CEF",
                table: "PlanesEstudio");

            migrationBuilder.RenameTable(
                name: "Carreras",
                schema: "CEF",
                newName: "Carreras");

            migrationBuilder.RenameTable(
                name: "PlanesEstudio",
                schema: "CEF",
                newName: "PlanEstudios");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Pruebas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<bool>(
                name: "EstadoCarrera",
                table: "Carreras",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClaveCarrera",
                table: "Carreras",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldFixedLength: true,
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "PlanEstudio",
                table: "PlanEstudios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(6)",
                oldFixedLength: true,
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "PerfilDeIngreso",
                table: "PlanEstudios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "PerfilDeEgreso",
                table: "PlanEstudios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "PlanEstudios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<bool>(
                name: "EstadoPlanEstudio",
                table: "PlanEstudios",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comentarios",
                table: "PlanEstudios",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CampoOcupacional",
                table: "PlanEstudios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanEstudios",
                table: "PlanEstudios",
                column: "IdPlanEstudio");

            migrationBuilder.CreateTable(
                name: "Docentes",
                columns: table => new
                {
                    IdDocente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroEmpleado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NombreDocente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaternoDocente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaternoDocente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailAlterno = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EstadoDocente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docentes", x => x.IdDocente);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_IdCoordinador",
                table: "Carreras",
                column: "IdCoordinador");

            migrationBuilder.CreateIndex(
                name: "IX_PlanEstudios_IdCarrera",
                table: "PlanEstudios",
                column: "IdCarrera");

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_Docentes_IdCoordinador",
                table: "Carreras",
                column: "IdCoordinador",
                principalTable: "Docentes",
                principalColumn: "IdDocente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanEstudios_Carreras_IdCarrera",
                table: "PlanEstudios",
                column: "IdCarrera",
                principalTable: "Carreras",
                principalColumn: "IdCarrera",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_Docentes_IdCoordinador",
                table: "Carreras");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanEstudios_Carreras_IdCarrera",
                table: "PlanEstudios");

            migrationBuilder.DropTable(
                name: "Docentes");

            migrationBuilder.DropIndex(
                name: "IX_Carreras_IdCoordinador",
                table: "Carreras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanEstudios",
                table: "PlanEstudios");

            migrationBuilder.DropIndex(
                name: "IX_PlanEstudios_IdCarrera",
                table: "PlanEstudios");

            migrationBuilder.EnsureSchema(
                name: "CEF");

            migrationBuilder.RenameTable(
                name: "Carreras",
                newName: "Carreras",
                newSchema: "CEF");

            migrationBuilder.RenameTable(
                name: "PlanEstudios",
                newName: "PlanesEstudio",
                newSchema: "CEF");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Pruebas",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "EstadoCarrera",
                schema: "CEF",
                table: "Carreras",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ClaveCarrera",
                schema: "CEF",
                table: "Carreras",
                type: "char(3)",
                fixedLength: true,
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "PlanEstudio",
                schema: "CEF",
                table: "PlanesEstudio",
                type: "nchar(6)",
                fixedLength: true,
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PerfilDeIngreso",
                schema: "CEF",
                table: "PlanesEstudio",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PerfilDeEgreso",
                schema: "CEF",
                table: "PlanesEstudio",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                schema: "CEF",
                table: "PlanesEstudio",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "EstadoPlanEstudio",
                schema: "CEF",
                table: "PlanesEstudio",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Comentarios",
                schema: "CEF",
                table: "PlanesEstudio",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "CampoOcupacional",
                schema: "CEF",
                table: "PlanesEstudio",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanesEstudio",
                schema: "CEF",
                table: "PlanesEstudio",
                column: "IdPlanEstudio");

            migrationBuilder.CreateIndex(
                name: "UK_Carreras_Alias",
                schema: "CEF",
                table: "Carreras",
                column: "AliasCarrera",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Carreras_Nombre",
                schema: "CEF",
                table: "Carreras",
                column: "NombreCarrera",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_PlanesEstudio_IdCarrera_Plan",
                schema: "CEF",
                table: "PlanesEstudio",
                columns: new[] { "IdCarrera", "PlanEstudio" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesEstudio_Carreras_IdCarrera",
                schema: "CEF",
                table: "PlanesEstudio",
                column: "IdCarrera",
                principalSchema: "CEF",
                principalTable: "Carreras",
                principalColumn: "IdCarrera",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
