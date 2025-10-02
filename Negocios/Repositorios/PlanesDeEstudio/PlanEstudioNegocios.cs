using Datos.IRepositorios.PlanesDeEstudio;
using Datos.Repositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.PlanEstudios;

namespace Negocios.Repositorios.PlanesDeEstudio;

public class PlanEstudioNegocios
{
    private readonly IPlanDeEstudioRepositorio _planRepositorio;

    public PlanEstudioNegocios(IPlanDeEstudioRepositorio planRepositorio)
    {
        _planRepositorio = planRepositorio;
    }

    // ================== CASOS DE USO ==================

    public async Task<ResultadoAcciones> InsertarPlanEstudio(E_PlanEstudio plan)
    {
        // La capa de servicio ya valida si es nulo, pero una doble verificación no hace daño.
        if (plan is null)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "El plan de estudio no contiene datos." } };
        }

        Normalizar(plan);
        plan.FechaCreacion = DateTime.Now; // Asignamos la fecha de creación aquí.

        var validacion = await ValidarPlanEstudio(plan, esModificacion: false);
        if (!validacion.Resultado) return validacion;

        return await _planRepositorio.InsertarPlanEstudio(plan);
    }

    public async Task<ResultadoAcciones> ModificarPlanEstudio(E_PlanEstudio plan)
    {
        if (plan is null || plan.IdPlanEstudio <= 0)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Los datos del plan de estudio son inválidos." } };
        }

        Normalizar(plan);

        var validacion = await ValidarPlanEstudio(plan, esModificacion: true);
        if (!validacion.Resultado) return validacion;

        return await _planRepositorio.ModificarPlanEstudio(plan);
    }

    public async Task<ResultadoAcciones> BorrarPlanEstudio(int idPlanEstudio)
    {
        if (idPlanEstudio <= 0)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "El identificador del plan de estudio no es válido." } };
        }

        // Regla de negocio: No se puede borrar algo que no existe.
        var planExistente = await _planRepositorio.BuscarPlanEstudio(idPlanEstudio);
        if (planExistente == null)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { "No se encontró el plan de estudio que se desea borrar." } };
        }

        // TODO: Agregar validación futura: no borrar si tiene materias asociadas.
        // if (await _materiaRepositorio.ExistenMateriasParaPlan(idPlanEstudio)) { ... }

        return await _planRepositorio.BorrarPlanEstudio(idPlanEstudio);
    }

    public async Task<ResultadoAccion<E_PlanEstudio>> ObtenerPlanEstudioPorId(int idPlanEstudio)
    {
        if (idPlanEstudio <= 0)
        {
            return new ResultadoAccion<E_PlanEstudio> { Resultado = false, Mensajes = { "El ID del plan de estudio no es válido." }, Entidad = null };
        }

        var entidad = await _planRepositorio.BuscarPlanEstudio(idPlanEstudio);
        if (entidad is null)
        {
            return new ResultadoAccion<E_PlanEstudio> { Resultado = false, Mensajes = { $"No se encontró el plan de estudio con ID {idPlanEstudio}." }, Entidad = null };
        }
        return new ResultadoAccion<E_PlanEstudio> { Resultado = true, Entidad = entidad };
    }

    public async Task<IEnumerable<E_PlanEstudio>> ListarPlanesEstudio(string? criterioBusqueda)
    {
        return await _planRepositorio.ListarPlanesEstudio(criterioBusqueda);
    }

    public async Task<IEnumerable<E_PlanEstudio>> ListarPlanesPorCarrera(int idCarrera)
    {
        return await _planRepositorio.ListarPlanesPorCarrera(idCarrera);
    }


    // ============== VALIDACIONES DE NEGOCIO ==============

    private async Task<ResultadoAcciones> ValidarPlanEstudio(E_PlanEstudio plan, bool esModificacion)
    {
        var res = new ResultadoAcciones { Resultado = true };

        // 1. Reglas de negocio que no requieren acceso a la BD
        ValidarCamposRequeridos(res, plan);
        ValidarLongitudes(res, plan);
        ValidarCoherenciaCreditos(res, plan);

        // Si ya hay errores, no tiene caso seguir consultando la BD.
        if (res.Mensajes.Count > 0)
        {
            res.Resultado = false;
            return res;
        }

        // 2. Reglas de unicidad que sí requieren acceso a la BD
        await ValidarUnicidadNombrePorCarrera(res, plan, esModificacion);

        // TODO: Si necesitas validar que IdCarrera e IdNivelAcademico existan, se haría aquí.
        // await ValidarExistenciaCarrera(res, plan.IdCarrera);

        if (res.Mensajes.Count > 0)
        {
            res.Resultado = false;
        }

        return res;
    }

    // --------- Normalización ---------
    private static void Normalizar(E_PlanEstudio p)
    {
        p.PlanEstudio = p.PlanEstudio?.Trim()!;
        p.PerfilDeIngreso = p.PerfilDeIngreso?.Trim()!;
        p.PerfilDeEgreso = p.PerfilDeEgreso?.Trim()!;
        p.CampoOcupacional = p.CampoOcupacional?.Trim()!;
        p.Comentarios = p.Comentarios?.Trim()!;
    }

    // --------- Reglas básicas (sin BD) ---------
    private static void ValidarCamposRequeridos(ResultadoAcciones res, E_PlanEstudio plan)
    {
        if (plan.IdCarrera <= 0) res.Mensajes.Add("La carrera es obligatoria.");
        if (string.IsNullOrWhiteSpace(plan.PlanEstudio)) res.Mensajes.Add("El nombre del plan de estudio es obligatorio.");
        if (string.IsNullOrWhiteSpace(plan.PerfilDeIngreso)) res.Mensajes.Add("El perfil de ingreso es obligatorio.");
        if (string.IsNullOrWhiteSpace(plan.PerfilDeEgreso)) res.Mensajes.Add("El perfil de egreso es obligatorio.");
        if (string.IsNullOrWhiteSpace(plan.CampoOcupacional)) res.Mensajes.Add("El campo ocupacional es obligatorio.");
        if (plan.IdNivelAcademico <= 0) res.Mensajes.Add("El nivel académico es obligatorio.");
    }

    private static void ValidarLongitudes(ResultadoAcciones res, E_PlanEstudio plan)
    {
        if (plan.PlanEstudio?.Length > 50) res.Mensajes.Add("El nombre del plan de estudio no debe exceder 50 caracteres.");
        if (plan.PerfilDeIngreso?.Length > 50) res.Mensajes.Add("El perfil de ingreso no debe exceder 50 caracteres.");
        if (plan.PerfilDeEgreso?.Length > 50) res.Mensajes.Add("El perfil de egreso no debe exceder 50 caracteres.");
        if (plan.CampoOcupacional?.Length > 50) res.Mensajes.Add("El campo ocupacional no debe exceder 50 caracteres.");
        if (plan.Comentarios?.Length > 250) res.Mensajes.Add("Los comentarios no deben exceder 250 caracteres.");
    }

    private static void ValidarCoherenciaCreditos(ResultadoAcciones res, E_PlanEstudio plan)
    {
        if (plan.TotalCreditos < 0 || plan.CreditosOptativos < 0 || plan.CreditosObligatorios < 0)
        {
            res.Mensajes.Add("Los valores de los créditos no pueden ser negativos.");
        }
        if (plan.CreditosObligatorios + plan.CreditosOptativos != plan.TotalCreditos)
        {
            res.Mensajes.Add("La suma de créditos obligatorios y optativos debe ser igual al total de créditos.");
        }
    }

    // --------- Reglas de unicidad (con BD) ---------
    private async Task ValidarUnicidadNombrePorCarrera(ResultadoAcciones res, E_PlanEstudio plan, bool esModificacion)
    {
        if (string.IsNullOrWhiteSpace(plan.PlanEstudio) || plan.IdCarrera <= 0) return;

        var existe = await _planRepositorio.ExistePlanEstudio(plan.PlanEstudio, plan.IdCarrera);
        if (!existe) return; // Si no existe, no hay conflicto.

        if (esModificacion)
        {
            // Si estamos modificando, debemos asegurarnos de que el conflicto no sea con el mismo registro.
            var planActual = await _planRepositorio.BuscarPlanEstudio(plan.IdPlanEstudio);
            // Si el plan actual tiene el mismo nombre y carrera, no es un error de unicidad, es el mismo registro.
            if (planActual != null &&
                planActual.PlanEstudio.Equals(plan.PlanEstudio, StringComparison.OrdinalIgnoreCase) &&
                planActual.IdCarrera == plan.IdCarrera)
            {
                return; // No hay conflicto, es el mismo plan.
            }
        }

        res.Mensajes.Add($"Ya existe un plan llamado '{plan.PlanEstudio}' para la carrera seleccionada.");
    }
}