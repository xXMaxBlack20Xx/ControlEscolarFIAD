
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;

namespace Datos.IRepositorios.PlanesDeEstudio;

public interface IPlanEstudioMateriaRepositorio
{
    // CRUD
    Task<ResultadoAcciones> AsignarMateria(E_PlanEstudioMateria asignacion);

    Task<ResultadoAcciones> DesasignarMateria(int idPlanEstudioMateria);

    Task<ResultadoAcciones> ModificarAsignacion(E_PlanEstudioMateria asignacion);

    // --- Consultas ---
    Task<IEnumerable<E_PlanEstudioMateria>> ListarMateriasPorPlan(int idPlanEstudio);

    Task<E_PlanEstudioMateria?> BuscarAsignacionPorId(int idPlanEstudioMateria);


    // --- Validaciones 
    Task<bool> ExisteAsignacion(int idPlanEstudio, int idMateria);

}