using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.Materias;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Entidades.PerfilesDTO.PlanesDeEstudio;

public class MateriaProfile : Profile
{
    public MateriaProfile()
    {
        // Mapeo de Entidad -> DTO
        CreateMap<E_Materias, MateriaDTO>();

        // Mapeo de DTO -> Entidad
        CreateMap<MateriaDTO, E_Materias>();
    }
}