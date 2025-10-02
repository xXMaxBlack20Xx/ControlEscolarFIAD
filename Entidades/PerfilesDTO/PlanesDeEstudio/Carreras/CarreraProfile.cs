/*
 * Perfil de AutoMapper para Planes de Estudio.
 *
 * - Esta clase hereda de AutoMapper.Profile y su CONSTRUCTOR debe llamarse exactamente igual que la clase
 *   (PlanEstudiosProfile). En ese constructor se configuran las reglas de mapeo.
 *
 * - CreateMap<E_PlanEstudio, PlanEstudioDTO>() registra el mapeo entre la ENTIDAD de dominio
 *   (usada por EF Core para la base de datos) y el DTO (usado en la capa de presentación/servicios).
 *
 * - ReverseMap() habilita el mapeo en ambos sentidos (Entidad ↔ DTO), evitando declarar dos mapas separados.
 *
 * - Si alguna propiedad difiere en nombre o formato, ajusta con ForMember(...).
 *
 * - Recuerda registrar este perfil en DI (ejemplo):
 * builder.Services.AddAutoMapper(typeof(CarreraProfile).Assembly);
 */

using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.Carreras;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Entidades.PerfilesDTO.PlanesDeEstudio.Carreras;

public class CarreraProfile : Profile
{
    public CarreraProfile()
    {
        // Define el mapeo en ambos sentidos (Entidad <--> DTO)
        CreateMap<E_Carrera, CarreraDTO>()
            // Mapeo especial para obtener el nombre del coordinador
            .ForMember(
                dest => dest.NombreCoordinador,
                opt => opt.MapFrom(src => src.Coordinador != null
                    ? $"{src.Coordinador.NombreDocente} {src.Coordinador.PaternoDocente} {src.Coordinador.MaternoDocente}".Trim()
                    : "No asignado")
            );

        CreateMap<CarreraDTO, E_Carrera>()
            .ForMember(dest => dest.IdCarrera, opt => opt.Ignore()) // Ignora el ID al crear/actualizar
            .ForMember(dest => dest.Coordinador, opt => opt.Ignore()) // Ignora el objeto de navegación
            .ForMember(dest => dest.PlanEstudios, opt => opt.Ignore()) // Ignora la colección de navegación
            // Sanitización de datos al mapear de DTO a Entidad
            .AfterMap((src, dest) =>
            {
                dest.ClaveCarrera = (src.ClaveCarrera ?? "").Trim().ToUpperInvariant();
                dest.NombreCarrera = (src.NombreCarrera ?? "").Trim();
                dest.AliasCarrera = (src.AliasCarrera ?? "").Trim();
            });
    }
}