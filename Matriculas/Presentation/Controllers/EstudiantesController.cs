using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Matriculas.Persistence.Models;
using Matriculas.Persistence.Context;
using Matriculas.Presentation.Response;
using Matriculas.Presentation.Dto;
using Matriculas.Service.Interface;

namespace Matriculas.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly AppContextDB _context;
        private readonly IEstudianteService _iestudianteService;

        public EstudiantesController(AppContextDB context, IEstudianteService iestudianteService)
        {
            _context = context;
            _iestudianteService = iestudianteService;
        }

        // GET: api/Estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiResponse<Estudiante>>>> Getestudiantes()
        {
            try
            {
                var estudiantes = await _iestudianteService.getAllEstudiantes();

                if (estudiantes == null)
                {
                    var emptyResponse = ApiResponse<IEnumerable<Estudiante>>.Error("No se pudo obtener la lista de estudiantes.");
                    return StatusCode(500, emptyResponse);
                }

                //Devuelve la lista del objeto estudiantes con todos sus campos
                var response = ApiResponse<IEnumerable<Estudiante>>.Success(estudiantes, "Lista de estudiantes");
                return Ok(response);
            }catch(DbUpdateException ex)
            {
                var response = ApiResponse<Estudiante>.Error("Ocurrio un error inesperado al listar los estudiantes");
                return StatusCode(500, response);
            }

        }

        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(long id)
        {
            var estudiante = await _context.estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return estudiante;
        }

        // PUT: api/Estudiantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(long id, Estudiante estudiante)
        {
            if (id != estudiante.EstudianteId)
            {
                return BadRequest();
            }

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estudiantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<EstudianteDTO>>> CreateEstudiante(EstudianteDTO estudianteDTO)
        {
            try
            {
                //Devuelve un mensaje exitoso y datos del estudiante para mostrar lo creado, pero no se expone ID ni Fecha de Creacion
                var estudiante = await _iestudianteService.CreateEstudiante(estudianteDTO);
                var response = ApiResponse<EstudianteDTO>.Success(estudiante, "Estudiante creado correctamente");
                return Ok(response);

            }catch(DbUpdateException ex)
            {
                var response = ApiResponse<EstudianteDTO>.Error("Ocurrio un error inesperado al crear un estudiante");
                return StatusCode(500, response);
            }
        }

        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(long id)
        {
            var estudiante = await _context.estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            _context.estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstudianteExists(long id)
        {
            return _context.estudiantes.Any(e => e.EstudianteId == id);
        }
    }
}
