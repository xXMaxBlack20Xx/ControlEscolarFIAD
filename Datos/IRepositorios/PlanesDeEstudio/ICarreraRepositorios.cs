using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Datos.IRepositorios.PlanesDeEstudio;

public interface ICarreraRepositorios
{
    Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera);
    Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera);
    Task<ResultadoAcciones> BorrarCarrera(int idCarrera);

    Task<E_Carrera?> BuscarCarrera(int idCarrera);

    // Listado general o filtrado por criterio
    Task<IEnumerable<E_Carrera>> ListarCarreras(string? criterioBusqueda = null);

    // Validaciones de unicidad
    Task<bool> ExisteClaveCarrera(string clave);
    Task<bool> ExisteNombreCarrera(string nombre);
    Task<bool> ExisteAliasCarrera(string alias);

    // Carreras ligadas a un plan de estudios
    Task<IEnumerable<E_Carrera>> ListarCarrerasPorPlanEstudio(int idPlanEstudio);
}