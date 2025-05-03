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

namespace Matriculas.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly AppContextDB _context;
        private readonly ICursoService _icursoService;

        public CursosController(AppContextDB context, ICursoService iCursoService)
        {
            _context = context;
            _icursoService = iCursoService;
        }

        // GET: api/Cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> Getcursos()
        {
            return await _context.cursos.ToListAsync();
        }

        // GET: api/Cursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(long id)
        {
            var curso = await _context.cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // PUT: api/Cursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(long id, Curso curso)
        {
            if (id != curso.CursoId)
            {
                return BadRequest();
            }

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // POST: api/Cursos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CursoDTO>>> CreateCurso(CursoDTO cursoDto)
        {
            try
            {
                var curso = await _icursoService.CreateCurso(cursoDto);
                return Ok(ApiResponse<CursoDTO>.Success(curso, "Curso creado correctamente"));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ApiResponse<CursoDTO>.Error("Ocurrio un error inesperado al querer crear un curso"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CursoDTO>.Error(ex.Message));
            }
        }

        // DELETE: api/Cursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(long id)
        {
            var curso = await _context.cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(long id)
        {
            return _context.cursos.Any(e => e.CursoId == id);
        }
    }
}
