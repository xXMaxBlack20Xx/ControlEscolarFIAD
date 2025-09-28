using Entidades.DTO.PlanesDeEstudio.Carreras;
using Entidades.Generales;

namespace Servicios.IRepositorios.PlanesDeEstudio;

public interface ICarreraServicios
{
    Task<ResultadoAcciones> InsertarCarrera(CarreraDTO carreraDTO);
    Task<ResultadoAcciones> BorrarCarrera(int idCarrera);
    Task<ResultadoAcciones> ModificarCarrera(CarreraDTO carreraDTO);

    Task<ResultadoAccion<T>> ObtenerCarrera<T>(int idCarrera) where T : class;

    Task<IEnumerable<CarreraDTO>> ListarCarreras(string? criterioBusqueda = null); // unificada y en DTOs
    Task<List<CarreraDTO>> ObtenerCarrerasPorPlanEstudioAsync(int idPlanEstudio);
}
