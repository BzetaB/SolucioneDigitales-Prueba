using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;

namespace Matriculas.Service.Interface
{
    public interface ICursoService
    {
        Task<Curso> GetCursoByName(string name);
        Task<CursoDTO> CreateCurso(CursoDTO cursoDto);
    }
}
