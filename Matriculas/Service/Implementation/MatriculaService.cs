using AutoMapper;
using Matriculas.Persistence.Context;
using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;
using Matriculas.Presentation.Request;
using Matriculas.Service.Interface;
using Matriculas.Utils.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Matriculas.Service.Implementation
{
    public class MatriculaService : IMatriculaService
    {
        private readonly AppContextDB _context;
        private readonly IMapper _mapper;
        private readonly IEstudianteService _estudianteService;
        private readonly ICursoService _cursoService;

        public MatriculaService(AppContextDB context, IMapper mapper, IEstudianteService estudianteService, ICursoService cursoService)
        {
            _context = context;
            _mapper = mapper;
            _estudianteService = estudianteService;
            _cursoService = cursoService;
        }

        public async Task<MatriculaDTO> createMatricula(CreateMatriculaRequest matriculaRequest)
        {

            Estudiante estudiante = await _estudianteService.getEstudianteById(matriculaRequest.EstudianteID);

            Curso curso = await _cursoService.GetCursoByName(matriculaRequest.CursoNombre);

            Matricula matricula = await this.findMatriculaByEstudianteAndCurso(estudiante.EstudianteId, curso.CursoId);
            if(matricula != null)
            {
                throw new Exception("El estudiante con ID: " + estudiante.EstudianteId + " ya se encuentra matriculado en el curso: " + curso.Nombre);
            }

            if (matriculaRequest.EnrollmentDate > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("La fecha de inscripcion no puede ser mayor al dia de hoy. Ingrese correctamente la fecha");
            }

            var newMatricula = new Matricula
            {
                EnrollmentDate = matriculaRequest.EnrollmentDate,
                Curso = curso,
                Estudiante = estudiante,
                Status = Estado.ACTIVA
            };

            _context.matriculas.Add(newMatricula);
            await _context.SaveChangesAsync();

            return _mapper.Map<MatriculaDTO>(newMatricula);
        }

        public async Task<bool> deleteMatricula(long idMatricula)
        {
            var matricula = await _context.matriculas.FindAsync(idMatricula) ?? throw new Exception("No se encontro la matricula con el ID: " + idMatricula);

            if(matricula.Status != Estado.CANCELADA)
            {
                throw new Exception("Solo se pueden eliminar matriculas con el estado 'CANCELADA'");
            }
            _context.matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Matricula?> findMatriculaByEstudianteAndCurso(long idEstudiante, long idCurso)
        {
            var matricula = await _context.matriculas.FromSqlRaw("EXEC sp_FindMatriculaByEstudianteIdAndCursoId @p0, @p1", idEstudiante, idCurso)
                .AsNoTracking()
                .ToListAsync();

            return matricula.FirstOrDefault();
        }

        public async Task<List<MatriculaListaDTO>> getAllMatriculaByEstado(string estado)
        {
            if (!Enum.TryParse<Estado>(estado, true, out var estadoEnum))
            {
                var valoresValidos = string.Join(", ", Enum.GetNames(typeof(Estado)));
                throw new Exception("No se encontro el estado: " + estado + "en la base de datos. Los estados validos son:" + valoresValidos);
            }

            var matricula = await _context.matriculas
            .Include(m => m.Estudiante)
            .Include(m => m.Curso)
            .Where(m => m.Status.ToString() == estado)
            .ToListAsync();

            if (matricula == null || !matricula.Any())
            {
                throw new Exception("No se encontró matrículas con el Estado: " + estado);
            }

            return _mapper.Map<List<MatriculaListaDTO>>(matricula);
        }

        public async Task<List<MatriculaListaDTO>> getAllMatriculaByIdEstudiante(long idEstudiante)
        {
            var matricula = await _context.matriculas
            .Include(m => m.Estudiante)
            .Include(m => m.Curso)
            .Where(m => m.EstudianteId == idEstudiante)
            .ToListAsync();

            if (matricula == null || !matricula.Any())
            {
                throw new Exception("No se encontró ninguna matrícula relacionada con el ID Estudiante: " + idEstudiante);
            }

            return _mapper.Map<List<MatriculaListaDTO>>(matricula);
        }

        public async Task<List<MatriculaListaDTO>> getAllMatriculas()
        {
            var matriculas = await _context.matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .ToListAsync();

            return _mapper.Map<List<MatriculaListaDTO>>(matriculas) ;
        }

        public async Task<List<MatriculaListaDTO>> getAllMatriculasByIdCurso(long idCurso)
        {
            var matricula = await _context.matriculas
            .Include(m => m.Estudiante)
            .Include(m => m.Curso)
            .Where(m => m.Curso.CursoId == idCurso)
            .ToListAsync();

            if (matricula == null || !matricula.Any())
            {
                throw new Exception("No se encontró ninguna matrícula relacionada con el ID Curso: " + idCurso);
            }

            return _mapper.Map<List<MatriculaListaDTO>>(matricula);
        }

        public async Task<MatriculaListaDTO> getMatriculaByIdMatricula(long idMatricula)
        {
            var matricula = await _context.matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .FirstOrDefaultAsync(m => m.MatriculaId == idMatricula)
                ?? throw new Exception("No se encontró la matrícula con el ID: " + idMatricula);

            return _mapper.Map<MatriculaListaDTO>(matricula);
        }

        public async Task<MatriculaDTO> updateMatricula(long idMatricula, UpdateMatriculaRequest updateMatricula)
        {
            if(idMatricula != updateMatricula.MatriculaId)
            {
                throw new Exception("El ID de la URL: " + idMatricula + " no coincide con el ID del cuerpo: " + updateMatricula.MatriculaId);
            }

            var resultado = new SqlParameter("@ResultadoMensaje", SqlDbType.NVarChar, 200)
            {
                Direction = ParameterDirection.Output,
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateMatriculaEstado @MatriculaId = {0}, @NuevoEstado = {1}, @ResultadoMensaje = @ResultadoMensaje OUTPUT",
                idMatricula, updateMatricula.Status, resultado);

            string mensaje = resultado.Value?.ToString();

            if(mensaje != "Actualización exitosa.")
            {
                throw new Exception(mensaje);
            }

            var matriculaActualizada = await _context
                .matriculas
                .Include(m => m.Curso)
                .FirstOrDefaultAsync(m => m.MatriculaId == idMatricula) 
                ?? throw new Exception("No se encontro la matricula luego de la actualizacion");

            return _mapper.Map<MatriculaDTO>(matriculaActualizada);
        }
    }
}
