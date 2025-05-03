namespace Matriculas.Presentation.Request
{
    public class CreateMatriculaRequest
    {
        public long EstudianteID { get; set; }
        public string CursoNombre { get; set; }
        public DateOnly EnrollmentDate { get; set; }
    }
}
