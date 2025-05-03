using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matriculas.Migrations
{
    /// <inheritdoc />
    public partial class AddAtributos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "estudiantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "estudiantes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "cursos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "estudiantes");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "estudiantes");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "cursos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "cursos");
        }
    }
}
