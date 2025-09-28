using AutoMapper;
using Entidades.DTO;
using Entidades.Modelos;

namespace Entidades.PerfilesDTO;

public class PruebaProfile : Profile
{
    public PruebaProfile()
    {
        CreateMap<E_Prueba, PruebaDTO>().ReverseMap();
    }
}