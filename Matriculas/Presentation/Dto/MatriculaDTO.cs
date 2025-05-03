using Matriculas.Utils.Enums;

namespace Matriculas.Presentation.Dto
{
    public class MatriculaDTO
    {
        public long EstudianteID { get; set; }
        public string CursoNombre { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public string Status { get; set; }

    }
}
