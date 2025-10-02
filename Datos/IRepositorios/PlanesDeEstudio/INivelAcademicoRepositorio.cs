using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.NivelesAcademicos;

namespace Datos.IRepositorios.PlanesDeEstudio;

public interface INivelAcademicoRepositorio
{
    // --- Operaciones CRUD ---

    /// <summary>
    /// Inserta un nuevo nivel académico en la base de datos.
    /// </summary>
    Task<ResultadoAcciones> InsertarNivelAcademico(E_NivelAcademico nivelAcademico);

    /// <summary>
    /// Modifica un nivel académico existente.
    /// </summary>
    Task<ResultadoAcciones> ModificarNivelAcademico(E_NivelAcademico nivelAcademico);

    /// <summary>
    /// Elimina un nivel académico por su ID.
    /// </summary>
    Task<ResultadoAcciones> BorrarNivelAcademico(int idNivelAcademico);


    // --- Operaciones de Consulta ---

    /// <summary>
    /// Busca un nivel académico por su ID.
    /// </summary>
    Task<E_NivelAcademico?> BuscarNivelAcademico(int idNivelAcademico);

    /// <summary>
    /// Obtiene una lista de todos los niveles académicos, opcionalmente filtrada.
    /// </summary>
    Task<IEnumerable<E_NivelAcademico>> ListarNivelesAcademicos(string? criterioBusqueda = null);


    // --- Operaciones de Validación ---

    /// <summary>
    /// Verifica si ya existe un nivel académico con el mismo nombre.
    /// Esencial para la lógica de 'Modificar' para no chocar consigo mismo.
    /// </summary>
    /// <param name="nombre">El nombre a verificar.</param>
    /// <param name="idExcluir">Opcional: El ID del nivel académico a excluir de la búsqueda.</param>
    Task<bool> ExisteNombreNivelAcademico(string nombre, int? idExcluir = null);

    /// <summary>
    /// Verifica si un nivel académico está siendo utilizado por algún plan de estudios.
    /// Crucial para impedir el borrado de registros en uso.
    /// </summary>
    Task<bool> EstaEnUso(int idNivelAcademico);
}
