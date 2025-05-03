using Matriculas.Utils.Enums;

namespace Matriculas.Persistence.Models
{
    public class Matricula
    {
        public long MatriculaId { get; set; }

        public DateOnly EnrollmentDate { get; set; }
        public Estado Status { get; set; }

        //Relaciones Unidireccionales
        public long EstudianteId { get; set; }
        public long CursoId { get; set; }

        public Curso Curso { get; set; }
        public Estudiante Estudiante { get; set; }
    }
}
