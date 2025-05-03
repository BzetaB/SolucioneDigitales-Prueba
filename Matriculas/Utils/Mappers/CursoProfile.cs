using AutoMapper;
using Matriculas.Persistence.Models;
using Matriculas.Presentation.Dto;

namespace Matriculas.Utils.Mappers
{
    public class CursoProfile : Profile
    {
        public CursoProfile() 
        {
            CreateMap<Curso, CursoDTO>();
            CreateMap<CursoDTO,Curso>();
        }
    }
}
