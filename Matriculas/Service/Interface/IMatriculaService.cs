using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;
using Matriculas.Presentation.Request;

namespace Matriculas.Service.Interface
{
    public interface IMatriculaService
    {
        Task<List<MatriculaListaDTO>> getAllMatriculas();
        Task<MatriculaListaDTO> getMatriculaByIdMatricula(long idMatricula);
        Task<List<MatriculaListaDTO>> getAllMatriculaByIdEstudiante(long idEstudiante);
        Task<List<MatriculaListaDTO>> getAllMatriculasByIdCurso(long idCurso);
        Task<List<MatriculaListaDTO>> getAllMatriculaByEstado(string estado);
        Task<Matricula?> findMatriculaByEstudianteAndCurso(long idEstudiante, long idCurso);
        Task<MatriculaDTO> createMatricula(CreateMatriculaRequest matriculaRequest);
        Task<MatriculaDTO> updateMatricula(long idMatricula, UpdateMatriculaRequest updateMatricula);
        Task<bool> deleteMatricula(long idMatricula);
    }
}
