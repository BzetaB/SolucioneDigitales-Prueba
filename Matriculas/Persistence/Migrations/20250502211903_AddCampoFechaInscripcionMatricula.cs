using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matriculas.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoFechaInscripcionMatricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "estado",
                table: "matriculas",
                newName: "Estado");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaInscripcion",
                table: "matriculas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaInscripcion",
                table: "matriculas");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "matriculas",
                newName: "estado");
        }
    }
}
