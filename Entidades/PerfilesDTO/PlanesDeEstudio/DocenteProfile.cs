using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.Docentes;
using Entidades.Modelos.PlanesDeEstudio.Docentes;

namespace Entidades.PerfilesDTO.PlanesDeEstudio;

public class DocenteProfile : Profile
{
    public DocenteProfile()
    {
        // Define el mapeo en ambos sentidos (Entidad <--> DTO)
        CreateMap<E_Docentes, DocenteDTO>()
            .ReverseMap() // Habilita el mapeo de DTO a Entidad
            .ForMember(dest => dest.IdDocente, opt => opt.Ignore()) // Ignora el ID al crear/actualizar desde un DTO
            .ForMember(dest => dest.CarrerasCoordinadas, opt => opt.Ignore()) // Ignora la propiedad de navegación

            // Sanitización de datos al mapear de DTO a Entidad
            .AfterMap((src, dest) =>
            {
                dest.NumeroEmpleado = (src.NumeroEmpleado ?? "").Trim();
                dest.NombreDocente = (src.NombreDocente ?? "").Trim();
                dest.PaternoDocente = (src.PaternoDocente ?? "").Trim();
                dest.MaternoDocente = (src.MaternoDocente ?? "").Trim();
                dest.EmailAlterno = (src.EmailAlterno ?? "").Trim().ToLowerInvariant();
            });
    }
}
