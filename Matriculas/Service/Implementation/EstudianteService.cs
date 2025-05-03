using AutoMapper;
using Matriculas.Persistence.Context;
using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;
using Matriculas.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Matriculas.Service.Implementation
{
    public class EstudianteService : IEstudianteService
    {
        private readonly AppContextDB _context; 
        private readonly IMapper _mapper;

        public EstudianteService(AppContextDB context, IMapper mapper) { 
            _context = context;
            _mapper = mapper;
        }

        public async Task<EstudianteDTO> CreateEstudiante(EstudianteDTO estudianteDto)
        {

            var estudiante = _mapper.Map<Estudiante>(estudianteDto);

            estudiante.FechaCreacion = DateTime.Now;

            _context.estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return _mapper.Map<EstudianteDTO>(estudiante);
        }

        public Task<bool> deleteEstudiante(long estudianteId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Estudiante>> getAllEstudiantes()
        {
            var estudiantes = await _context.estudiantes.ToListAsync();
            return estudiantes;
        }

        public async Task<Estudiante> getEstudianteById(long estudianteId)
        {
            var estudiante = await _context.estudiantes.FindAsync(estudianteId);

            return estudiante == null ? throw new Exception("No se encontro el estudiante con el ID: " + estudianteId) : estudiante;
        }

        public Task<EstudianteDTO> updateEstudiante(EstudianteDTO estudiante, long id)
        {
            throw new NotImplementedException();
        }
    }
}
