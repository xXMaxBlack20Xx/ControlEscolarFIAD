using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.PlanEstudios;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositorios.PlanesDeEstudio;

public class PlanEstudioRepositorio(ContextDB db) : IPlanDeEstudioRepositorio
{
    private readonly ContextDB _db = db;

    // ========================= Helpers =========================

    private static string N(string? s) => (s ?? string.Empty).Trim().ToUpperInvariant();

    private async Task<bool> ExistePlanEstudioInterno(string planEstudio, int idCarrera, int? excluirId = null)
    {
        var q = db.PlanEstudios.AsQueryable();
        if (excluirId is not null)
            q = q.Where(p => p.IdPlanEstudio != excluirId.Value);

        var planN = N(planEstudio);
        return await q.AnyAsync(p => p.PlanEstudio == planN && p.IdCarrera == idCarrera);
    }

    private async Task<ResultadoAcciones> ValidarPlan(E_PlanEstudio plan, bool esUpdate = false)
    {
        var res = new ResultadoAcciones { Resultado = true };

        plan.PlanEstudio = N(plan.PlanEstudio);

        int? excluirId = esUpdate ? plan.IdPlanEstudio : null;

        if (string.IsNullOrWhiteSpace(plan.PlanEstudio))
        {
            res.Mensajes.Add("El plan de estudio es obligatorio.");
            res.Resultado = false;
        }
        else if (await ExistePlanEstudioInterno(plan.PlanEstudio, plan.IdCarrera, excluirId))
        {
            res.Mensajes.Add("Ya existe un plan de estudio con el mismo código en esta carrera.");
            res.Resultado = false;
        }

        // --> CAMBIO: Validar que el Nivel Académico sea válido
        if (plan.IdNivelAcademico <= 0)
        {
            res.Mensajes.Add("Debe seleccionar un nivel académico válido.");
            res.Resultado = false;
        }

        if (plan.TotalCreditos != plan.CreditosOptativos + plan.CreditosObligatorios)
        {
            res.Mensajes.Add("La suma de créditos optativos y obligatorios debe coincidir con el total de créditos.");
            res.Resultado = false;
        }

        return res;
    }

    // ========================= CRUD =========================

    public async Task<ResultadoAcciones> InsertarPlanEstudio(E_PlanEstudio planDeEstudio)
    {
        try
        {
            var res = await ValidarPlan(planDeEstudio, esUpdate: false);
            if (!res.Resultado) return res;

            await db.PlanEstudios.AddAsync(planDeEstudio);
            await db.SaveChangesAsync();

            res.Resultado = true;
            res.Mensajes.Add("Plan de estudio insertado correctamente.");
            return res;
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones
            {
                Resultado = false,
                Mensajes = { $"Error al insertar plan de estudio: {ex.Message}" }
            };
        }
    }

    public async Task<ResultadoAcciones> ModificarPlanEstudio(E_PlanEstudio planDeEstudio)
    {
        try
        {
            var existente = await db.PlanEstudios
                .FirstOrDefaultAsync(p => p.IdPlanEstudio == planDeEstudio.IdPlanEstudio);

            if (existente is null)
            {
                return new ResultadoAcciones
                {
                    Resultado = false,
                    Mensajes = { $"El plan de estudio con ID {planDeEstudio.IdPlanEstudio} no fue encontrado." }
                };
            }

            // Copia valores a actualizar
            existente.PlanEstudio = N(planDeEstudio.PlanEstudio);
            existente.TotalCreditos = planDeEstudio.TotalCreditos;
            existente.CreditosOptativos = planDeEstudio.CreditosOptativos;
            existente.CreditosObligatorios = planDeEstudio.CreditosObligatorios;
            existente.PerfilDeIngreso = planDeEstudio.PerfilDeIngreso?.Trim() ?? string.Empty;
            existente.PerfilDeEgreso = planDeEstudio.PerfilDeEgreso?.Trim() ?? string.Empty;
            existente.CampoOcupacional = planDeEstudio.CampoOcupacional?.Trim() ?? string.Empty;
            existente.Comentarios = planDeEstudio.Comentarios?.Trim() ?? string.Empty;
            existente.EstadoPlanEstudio = planDeEstudio.EstadoPlanEstudio;
            existente.IdCarrera = planDeEstudio.IdCarrera;
            existente.IdNivelAcademico = planDeEstudio.IdNivelAcademico; // --> CAMBIO: Actualizar el IdNivelAcademico

            var res = await ValidarPlan(existente, esUpdate: true);
            if (!res.Resultado) return res;

            await db.SaveChangesAsync();

            res.Resultado = true;
            res.Mensajes.Add("Plan de estudio modificado correctamente.");
            return res;
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones
            {
                Resultado = false,
                Mensajes = { $"Error al modificar plan de estudio: {ex.Message}" }
            };
        }
    }

    public async Task<ResultadoAcciones> BorrarPlanEstudio(int idPlanEstudio)
    {
        try
        {
            var plan = await db.PlanEstudios.FindAsync(idPlanEstudio);
            if (plan is null)
            {
                return new ResultadoAcciones
                {
                    Resultado = false,
                    Mensajes = { "No se encontró el plan de estudio." }
                };
            }

            db.PlanEstudios.Remove(plan);
            await db.SaveChangesAsync();

            return new ResultadoAcciones
            {
                Resultado = true,
                Mensajes = { "Plan de estudio eliminado correctamente." }
            };
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones
            {
                Resultado = false,
                Mensajes = { $"Error al eliminar plan de estudio: {ex.Message}" }
            };
        }
    }

    // ========================= Consultas =========================

    public async Task<E_PlanEstudio?> BuscarPlanEstudio(int idPlanEstudio)
        => await db.PlanEstudios
            .AsNoTracking()
            .Include(p => p.Carrera)
            .Include(p => p.NivelAcademico) // --> CAMBIO: Incluir Nivel Académico
            .FirstOrDefaultAsync(p => p.IdPlanEstudio == idPlanEstudio);

    public async Task<IEnumerable<E_PlanEstudio>> ListarPlanesEstudio(string? criterioBusqueda = null)
    {
        var q = db.PlanEstudios
            .AsNoTracking()
            .Include(p => p.Carrera)
            .Include(p => p.NivelAcademico) // --> CAMBIO: Incluir Nivel Académico
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(criterioBusqueda))
        {
            var term = criterioBusqueda.Trim();
            q = q.Where(p =>
                EF.Functions.Like(p.PlanEstudio, $"%{term}%") ||
                EF.Functions.Like(p.Carrera.NombreCarrera, $"%{term}%") ||
                EF.Functions.Like(p.NivelAcademico.NombreNivelAcademico, $"%{term}%")); // --> CAMBIO: Incluir búsqueda por nivel
        }

        return await q.ToListAsync();
    }

    public async Task<IEnumerable<E_PlanEstudio>> ListarPlanesPorCarrera(int idCarrera)
        => await db.PlanEstudios
            .AsNoTracking()
            .Include(p => p.NivelAcademico) // --> CAMBIO: Incluir Nivel Académico para mostrarlo en la lista
            .Where(p => p.IdCarrera == idCarrera)
            .ToListAsync();

    // ========================= Validaciones =========================

    public async Task<bool> ExistePlanEstudio(string planEstudio, int idCarrera)
        => await ExistePlanEstudioInterno(planEstudio, idCarrera);
}