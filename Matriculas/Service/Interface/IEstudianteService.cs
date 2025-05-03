using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;

namespace Matriculas.Service.Interface
{
    public interface IEstudianteService
    {
        Task<List<Estudiante>> getAllEstudiantes();
        Task<Estudiante> getEstudianteById(long estudianteId);
        Task<EstudianteDTO> CreateEstudiante(EstudianteDTO estudianteDto);
        Task<EstudianteDTO> updateEstudiante(EstudianteDTO estudiante, long id);
        Task<bool> deleteEstudiante(long estudianteId);
    }
}
