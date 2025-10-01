using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Datos.IRepositorios.PlanesDeEstudio;

public interface ICarreraRepositorios
{
    // CRUD
    Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera);
    Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera);
    Task<ResultadoAcciones> BorrarCarrera(int idCarrera);

    // Busca una carrera por su ID e incluye la información de su Docente coordinador.
    Task<E_Carrera?> BuscarCarreraConCoordinador(int idCarrera);

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