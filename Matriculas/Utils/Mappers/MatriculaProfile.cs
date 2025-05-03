using AutoMapper;
using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;
using Matriculas.Presentation.Request;

namespace Matriculas.Utils.Mappers
{
    public class MatriculaProfile : Profile
    {
        public MatriculaProfile() 
        {
            CreateMap<Matricula, MatriculaDTO>();
            CreateMap<CreateMatriculaRequest,Matricula>();
            CreateMap<Matricula, MatriculaListaDTO>()
                .ForMember(dest => dest.EstudianteNombre, opt => opt.MapFrom(src => src.Estudiante.Nombre))
                .ForMember(dest => dest.EstudianteApellido, opt => opt.MapFrom(src => src.Estudiante.Apellido))
                .ForMember(dest => dest.CursoNombre, opt => opt.MapFrom(src => src.Curso.Nombre))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); ;
        }
    }
}
