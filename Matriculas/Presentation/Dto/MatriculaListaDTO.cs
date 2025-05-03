namespace Matriculas.Presentation.Dto
{
    public class MatriculaListaDTO
    {
        public long MatriculaId { get; set; }
        public long EstudianteId { get; set; }
        public string EstudianteNombre { get; set; }
        public string EstudianteApellido { get; set; }
        public long CursoId { get; set; }
        public string CursoNombre { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public string Status { get; set; }
    }
}
