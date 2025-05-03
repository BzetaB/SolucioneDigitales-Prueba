using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matriculas.Migrations
{
    /// <inheritdoc />
    public partial class addEnumsEstadoMatricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaInscripcion",
                table: "matriculas",
                newName: "EnrollmentDate");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "matriculas",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "matriculas",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "matriculas",
                newName: "FechaInscripcion");
        }
    }
}
