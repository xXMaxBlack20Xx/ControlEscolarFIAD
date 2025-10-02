
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Datos.IRepositorios.PlanesDeEstudio;

public interface IMateriaRepositorio
{
    // CRUD 
    Task<ResultadoAcciones> InsertarMateria(E_Materias materia);

    Task<ResultadoAcciones> ModificarMateria(E_Materias materia);

    Task<ResultadoAcciones> BorrarMateria(int idMateria);

    // --- Consultas ---
    Task<E_Materias?> BuscarMateria(int idMateria);

    Task<IEnumerable<E_Materias>> ListarMaterias(string? criterioBusqueda = null);

    // --- Validaciones ---
    Task<bool> ExisteMateria(string claveMateria);
}