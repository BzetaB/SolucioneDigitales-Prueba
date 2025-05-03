namespace Matriculas.Persistence.Models
{
    public class Estudiante
    {
        public long EstudianteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
