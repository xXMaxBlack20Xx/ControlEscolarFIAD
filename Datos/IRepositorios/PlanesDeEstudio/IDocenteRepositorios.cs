using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Docentes;

namespace Datos.IRepositorios.PlanesDeEstudio;
public interface IDocenteRepositorios
{
    // --- Operaciones CRUD ---
    Task<ResultadoAcciones> InsertarDocente(E_Docentes docente);
    Task<ResultadoAcciones> ModificarDocente(E_Docentes docente);
    Task<ResultadoAcciones> BorrarDocente(int idDocente);

    // --- Consultas ---
    Task<E_Docentes?> BuscarDocente(int idDocente);

    /// <summary>
    /// Obtiene una lista de todos los docentes o filtra por un criterio de búsqueda.
    /// El criterio puede aplicar a nombre, apellidos, número de empleado o email.
    /// </summary>
    Task<IEnumerable<E_Docentes>> ListarDocentes(string? criterioBusqueda = null);

    // --- Validaciones de Unicidad ---
    /// <summary>
    /// Verifica si ya existe un docente con el mismo número de empleado.
    /// Opcionalmente, puede excluir un IdDocente (útil al modificar).
    /// </summary>
    Task<bool> ExisteNumeroEmpleado(string numeroEmpleado, int? idDocenteExcluir = null);

    /// <summary>
    /// Verifica si ya existe un docente con el mismo email.
    /// Opcionalmente, puede excluir un IdDocente (útil al modificar).
    /// </summary>
    Task<bool> ExisteEmail(string email, int? idDocenteExcluir = null);

    // --- Validaciones de Relaciones ---
    /// <summary>
    /// Verifica si un docente está asignado como coordinador a alguna carrera activa.
    /// </summary>
    Task<bool> EsCoordinadorDeCarrera(int idDocente);
}