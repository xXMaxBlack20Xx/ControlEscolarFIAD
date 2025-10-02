using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.PlanEstudioMaterias;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositorios.PlanesDeEstudio;

public class PlanEstudioMateriaRepositorio(ContextDB db) : IPlanEstudioMateriaRepositorio
{
    private readonly ContextDB _db = db;

    // ========================= Helpers =========================

    private async Task<bool> ExisteAsignacionInterno(int idPlanEstudio, int idMateria, int? excluirId = null)
    {
        var q = _db.PlanesEstudioMateria.AsQueryable();
        if (excluirId is not null)
            q = q.Where(pem => pem.IdPlanEstudioMateria != excluirId.Value);

        return await q.AnyAsync(pem => pem.IdPlanEstudio == idPlanEstudio && pem.IdMateria == idMateria);
    }

    // ========================= Operaciones =========================

    public async Task<ResultadoAcciones> AsignarMateria(E_PlanEstudioMateria asignacion)
    {
        try
        {
            if (await ExisteAsignacionInterno(asignacion.IdPlanEstudio, asignacion.IdMateria))
            {
                return ResultadoAcciones.Fallido("Esta materia ya está asignada a este plan de estudio.");
            }

            await _db.PlanesEstudioMateria.AddAsync(asignacion);
            await _db.SaveChangesAsync();

            return ResultadoAcciones.Exitoso("Materia asignada correctamente al plan de estudio.");
        }
        catch (Exception ex)
        {
            return ResultadoAcciones.Fallido($"Error al asignar la materia: {ex.Message}");
        }
    }

    public async Task<ResultadoAcciones> ModificarAsignacion(E_PlanEstudioMateria asignacion)
    {
        try
        {
            var existente = await _db.PlanesEstudioMateria.FindAsync(asignacion.IdPlanEstudioMateria);
            if (existente is null)
            {
                return ResultadoAcciones.Fallido("No se encontró la asignación para modificar.");
            }

            // Valida si al cambiar los IDs se genera un duplicado (aunque esto no debería pasar en la UI)
            if (existente.IdPlanEstudio != asignacion.IdPlanEstudio || existente.IdMateria != asignacion.IdMateria)
            {
                if (await ExisteAsignacionInterno(asignacion.IdPlanEstudio, asignacion.IdMateria))
                {
                    return ResultadoAcciones.Fallido("Esta materia ya está asignada a este plan de estudio.");
                }
            }

            existente.Semestre = asignacion.Semestre;
            existente.Estado = asignacion.Estado;

            await _db.SaveChangesAsync();
            return ResultadoAcciones.Exitoso("Asignación modificada correctamente.");
        }
        catch (Exception ex)
        {
            return ResultadoAcciones.Fallido($"Error al modificar la asignación: {ex.Message}");
        }
    }

    public async Task<ResultadoAcciones> DesasignarMateria(int idPlanEstudioMateria)
    {
        try
        {
            var asignacion = await _db.PlanesEstudioMateria.FindAsync(idPlanEstudioMateria);
            if (asignacion is null)
            {
                return ResultadoAcciones.Fallido("No se encontró la asignación a eliminar.");
            }

            _db.PlanesEstudioMateria.Remove(asignacion);
            await _db.SaveChangesAsync();

            return ResultadoAcciones.Exitoso("Materia desasignada correctamente del plan de estudio.");
        }
        catch (Exception ex)
        {
            return ResultadoAcciones.Fallido($"Error al desasignar la materia: {ex.Message}");
        }
    }

    // ========================= Consultas =========================

    public async Task<IEnumerable<E_PlanEstudioMateria>> ListarMateriasPorPlan(int idPlanEstudio)
    {
        return await _db.PlanesEstudioMateria
            .AsNoTracking()
            .Include(pem => pem.Materia) // Eager loading para obtener los datos de la materia
            .Where(pem => pem.IdPlanEstudio == idPlanEstudio)
            .OrderBy(pem => pem.Semestre)
            .ThenBy(pem => pem.Materia.NombreMateria)
            .ToListAsync();
    }

    public async Task<E_PlanEstudioMateria?> BuscarAsignacionPorId(int idPlanEstudioMateria)
    {
        return await _db.PlanesEstudioMateria
            .AsNoTracking()
            .Include(pem => pem.Materia)
            .Include(pem => pem.PlanEstudio)
            .FirstOrDefaultAsync(pem => pem.IdPlanEstudioMateria == idPlanEstudioMateria);
    }

    // ========================= Validaciones =========================

    public async Task<bool> ExisteAsignacion(int idPlanEstudio, int idMateria) => await ExisteAsignacionInterno(idPlanEstudio, idMateria);
}