using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.PlanEstudios;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Entidades.PerfilesDTO.PlanesDeEstudio;

public class PlanEstudiosProfile : Profile
{
    public PlanEstudiosProfile()
    {
        // ENTIDAD -> DTO
        CreateMap<E_PlanEstudio, PlanEstudioDTO>()
            .ForMember(d => d.FechaCreacion, o => o.MapFrom(s => (DateTime?)s.FechaCreacion))
            // NO mapear CarreraNombre porque no existe en el DTO
            .ForMember(d => d.IdCarrera, o => o.MapFrom(s => s.IdCarrera));

        // DTO -> ENTIDAD
        CreateMap<PlanEstudioDTO, E_PlanEstudio>()
            .ForMember(e => e.Carrera, o => o.Ignore()) // evitar inserts accidentales
            .ForMember(e => e.FechaCreacion, o => o.MapFrom(d => d.FechaCreacion ?? DateTime.UtcNow));
    }
}