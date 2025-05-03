using System.ComponentModel.DataAnnotations;

namespace Matriculas.Presentation.Dto
{
    public class EstudianteDTO
    {
        [Required(ErrorMessage = "El Nombre del estudiante es requerido")]
        [MinLength(3, ErrorMessage = "El campo no cumple con los caracteres minimos")]
        [StringLength(50, ErrorMessage = "El campo excede los caracteres maximos")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido del estudiante es requerido")]
        [MinLength(3, ErrorMessage = "El campo no cumple con los caracteres minimos")]
        [StringLength(50, ErrorMessage = "El campo excede los caracteres maximos")]
        public string Apellido { get; set; }
    }
}
