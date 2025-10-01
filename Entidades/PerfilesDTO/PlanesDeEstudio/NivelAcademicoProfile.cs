
using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.NivelesAcademicos;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Entidades.PerfilesDTO.PlanesDeEstudio;

public class NivelAcademicoProfile : Profile
{
    public NivelAcademicoProfile()
    {
        // Mapeo en ambios sentidos
        CreateMap<E_NivelAcademico, NivelAcademicoDTO>().ReverseMap()
            .ForMember(entidad => entidad.PlanesEstudio, opt => opt.Ignore())
            .ForMember(entidad => entidad.NombreNivelAcademico, opt => opt.MapFrom(dto => dto.NombreNivelAcademico.Trim()));
    }
}
