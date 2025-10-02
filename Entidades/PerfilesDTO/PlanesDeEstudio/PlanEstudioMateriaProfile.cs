
using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.PlanEstudioMaterias;
using Entidades.Modelos.PlanesDeEstudio.PlanEstudioMaterias;

namespace Entidades.PerfilesDTO.PlanesDeEstudio;

public class PlanEstudioMateriaProfile : Profile
{
    public PlanEstudioMateriaProfile()
    {
        // Mapeo entre PlanEstudioMateria y PlanEstudioMateriaDTO
        CreateMap<E_PlanEstudioMateria, PlanEstudioMateriaDTO>()
            .ForMember(dto => dto.NombrePlanEstudio,
                        opt => opt.MapFrom(entidad => entidad.PlanEstudio.PlanEstudio))
            .ForMember(dto => dto.ClaveMateria,
                        opt => opt.MapFrom(entidad => entidad.Materia.ClaveMateria))
            .ForMember(dto => dto.NombreMateria,
                        opt => opt.MapFrom(entidad => entidad.Materia.NombreMateria))
            .ForMember(dto => dto.CreditosMateria,
                        opt => opt.MapFrom(entidad => entidad.Materia.CR));

        // --- Mapeo de DTO -> ENTIDAD ---
        // Al convertir un DTO a una Entidad para guardar en la BD,
        // debemos ignorar las propiedades de navegación.
        // Solo nos interesan los IDs (IdPlanEstudio, IdMateria), no los objetos completos.
        CreateMap<PlanEstudioMateriaDTO, E_PlanEstudioMateria>()
            .ForMember(entidad => entidad.PlanEstudio, opt => opt.Ignore())
            .ForMember(entidad => entidad.Materia, opt => opt.Ignore());

    }
}