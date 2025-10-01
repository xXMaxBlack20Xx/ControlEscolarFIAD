using Entidades.DTO.PlanesDeEstudio.PlanEstudios;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Servicios.IRepositorios.PlanesDeEstudio;
public interface IPlanEstudioServicios
{
    Task<ResultadoAcciones> InsertarPlanEstudio(PlanEstudioDTO dto);
    Task<ResultadoAcciones> ModificarPlanEstudio(PlanEstudioDTO dto);
    Task<ResultadoAcciones> BorrarPlanEstudio(int idPlanEstudio);
    Task<ResultadoAccion<PlanEstudioDTO>> ObtenerPlanEstudio(int idPlanEstudio);
    Task<ResultadoAccion<T>> ObtenerPlanEstudio<T>(int idPlanEstudio) where T : class;
    Task<IEnumerable<PlanEstudioDTO>> ListarPlanesEstudio(string? criterioBusqueda = null);
    Task<List<PlanEstudioDTO>> ListarPlanesPorCarreraAsync(int idCarrera);
}