using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Matriculas.Persistence.Models;
using Matriculas.Persistence.Context;
using Matriculas.Service.Interface;
using Matriculas.Presentation.Response;
using Matriculas.Presentation.Dto;
using Matriculas.Presentation.Request;

namespace Matriculas.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculaService _imatriculaService;

        public MatriculasController(IMatriculaService imatriculaService)
        {
            _imatriculaService = imatriculaService;
        }

        // GET: api/Matriculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatriculaListaDTO>>> Getmatriculas()
        {
            try
            {
                var matriculas = await _imatriculaService.getAllMatriculas();
                if (matriculas == null)
                {
                    var emptyResponse = ApiResponse<IEnumerable<MatriculaListaDTO>>.Error("No se pudo obtener la lista de matriculas.");
                    return StatusCode(500, emptyResponse);
                }
                //Devuelve la lista del objeto matricula con todos sus campos
                var response = ApiResponse<IEnumerable<MatriculaListaDTO>>.Success(matriculas, "Lista de Matriculas");
                return Ok(response);
            }
            catch (DbUpdateException ex)
            {
                var response = ApiResponse<MatriculaListaDTO>.Error("Ocurrio un error inesperado al listar las matriculas");
                return StatusCode(500, response);
            }
        }

        // GET: api/Matriculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<MatriculaListaDTO>>> GetMatricula(long id)
        {
            try
            {
                var matricula = await _imatriculaService.getMatriculaByIdMatricula(id);
                return Ok(ApiResponse<MatriculaListaDTO>.Success(matricula, "Matricula encontrado satisfactoriamente"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<MatriculaListaDTO>.Error(ex.Message));
            }
        }

        [HttpGet("curso/{idCurso}")]
        public async Task<ActionResult<ApiResponse<List<MatriculaListaDTO>>>> GetMatriculaByIdCurso(long idCurso)
        {
            try
            {
                var matricula = await _imatriculaService.getAllMatriculasByIdCurso(idCurso);
                return Ok(ApiResponse<List<MatriculaListaDTO>>.Success(matricula, "Lista de Matriculas por ID Curso"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<MatriculaListaDTO>>.Error(ex.Message));
            }
        }

        [HttpGet("estudiante/{idEstudiante}")]
        public async Task<ActionResult<ApiResponse<List<MatriculaListaDTO>>>> GetMatriculaByIdEstudiante(long idEstudiante)
        {
            try
            {
                var matricula = await _imatriculaService.getAllMatriculaByIdEstudiante(idEstudiante);
                return Ok(ApiResponse<List<MatriculaListaDTO>>.Success(matricula, "Lista de Matriculas por ID Estudiante"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<MatriculaListaDTO>>.Error(ex.Message));
            }
        }

        [HttpGet("estado/{estado}")]
        public async Task<ActionResult<ApiResponse<List<MatriculaListaDTO>>>> GetMatriculaByIdEstudiante(string estado)
        {
            try
            {
                var matricula = await _imatriculaService.getAllMatriculaByEstado(estado);
                return Ok(ApiResponse<List<MatriculaListaDTO>>.Success(matricula, "Lista de Matriculas por Estado"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<MatriculaListaDTO>>.Error(ex.Message));
            }
        }

        // PUT: api/Matriculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<MatriculaDTO>> UpdateMatricula(long id, UpdateMatriculaRequest matriculaRequest)
        {
            try
            {
                var matricula = await _imatriculaService.updateMatricula(id, matriculaRequest);
                return Ok(ApiResponse<MatriculaDTO>.Success(matricula, "Matricula actualizada correctamente"));
            }
            catch (DbUpdateException ex)
            {
                var response = ApiResponse<MatriculaDTO>.Error("Ocurrio un error inesperado al actualizar una matricula");
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<MatriculaDTO>.Error(ex.Message));
            }
        }

        // POST: api/Matriculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<MatriculaDTO>>> CreateMatricula(CreateMatriculaRequest matriculaRequest)
        {
            try
            {
                var matricula = await _imatriculaService.createMatricula(matriculaRequest);
                return Ok(ApiResponse<MatriculaDTO>.Success(matricula, "Matricula creado correctamente"));
            }
            catch (DbUpdateException ex)
            {
                var response = ApiResponse<MatriculaDTO>.Error("Ocurrio un error inesperado al crear una matricula");
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<MatriculaDTO>.Error(ex.Message));
            }
        }

        // DELETE: api/Matriculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatricula(long id)
        {
            try
            {
                bool resultado = await _imatriculaService.deleteMatricula(id);
                return Ok(new ApiResponse<bool>(true, default, "Matrícula eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<bool>(false, false, ex.Message));
            }
        }
    }
}
