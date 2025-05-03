using AutoMapper;
using Matriculas.Persistence.Context;
using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;
using Matriculas.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Matriculas.Service.Implementation
{
    public class CursoService : ICursoService
    {
        private readonly AppContextDB _context;
        private readonly IMapper _mapper;
        
        public CursoService(AppContextDB context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CursoDTO> CreateCurso(CursoDTO cursoDto)
        {
            if (_context.cursos.FirstOrDefaultAsync(c => c.Nombre == cursoDto.Nombre) != null)
            {
                throw new Exception("Ya hay un curso existente con el nombre: " + cursoDto.Nombre);
            }

            var curso = _mapper.Map<Curso>(cursoDto);
            curso.FechaCreacion = DateTime.Now;

            _context.cursos.Add(curso);
            await _context.SaveChangesAsync();

            return _mapper.Map<CursoDTO>(curso);
        }

        public async Task<Curso> GetCursoByName(string name)
        {
            var curso = await _context.cursos.FirstOrDefaultAsync(c => c.Nombre == name);

            return curso == null ? throw new Exception("No se encontro el curso con el nombre: " + name) : curso;
        }
    }
}
