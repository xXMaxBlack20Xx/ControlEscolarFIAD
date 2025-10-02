using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.PlanEstudios;

namespace Datos.IRepositorios.PlanesDeEstudio;

public interface IPlanDeEstudioRepositorio
{
    // CRUD
    Task<ResultadoAcciones> InsertarPlanEstudio(E_PlanEstudio planDeEstudio);
    Task<ResultadoAcciones> ModificarPlanEstudio(E_PlanEstudio planDeEstudio);
    Task<ResultadoAcciones> BorrarPlanEstudio(int idPlanEstudio);

    // Consultas
    Task<E_PlanEstudio?> BuscarPlanEstudio(int idPlanEstudio);
    Task<IEnumerable<E_PlanEstudio>> ListarPlanesEstudio(string? criterioBusqueda = null);

    // Listar por Carrera
    Task<IEnumerable<E_PlanEstudio>> ListarPlanesPorCarrera(int idCarrera);

    // Validaciones
    Task<bool> ExistePlanEstudio(string planEstudio, int idCarrera);
}
