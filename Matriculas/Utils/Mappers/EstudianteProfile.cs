using AutoMapper;
using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;

namespace Matriculas.Utils.Mappers
{
    public class EstudianteProfile : Profile
    {
        public EstudianteProfile() 
        {
            CreateMap<Estudiante,EstudianteDTO>();
            CreateMap<EstudianteDTO, Estudiante>();
        }
    }
}
