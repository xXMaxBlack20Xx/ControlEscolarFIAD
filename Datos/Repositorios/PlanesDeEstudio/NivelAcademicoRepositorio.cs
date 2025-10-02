
using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.NivelesAcademicos;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositorios.PlanesDeEstudio;

public class NivelAcademicoRepositorio(ContextDB db) : INivelAcademicoRepositorio
{
    private readonly ContextDB _db = db;

    // ========================= Helpers =========================

    // --- MEJORA: Añadimos ToUpperInvariant() para validaciones case-insensitive ---
    private static string Normalize(string? s) => (s ?? string.Empty).Trim().ToUpperInvariant();

    private async Task<bool> ExisteNombreNivelAcademicoInterno(string nombre, int? idExcluir = null)
    {
        var q = _db.NivelesAcademicos.AsQueryable();
        if (idExcluir.HasValue)
        {
            q = q.Where(n => n.IdNivelAcademico != idExcluir.Value);
        }

        // Ahora, la comparación será insensible a mayúsculas y minúsculas
        var nombreNormalizado = Normalize(nombre);
        return await q.AnyAsync(n => n.NombreNivelAcademico.ToUpper() == nombreNormalizado);
    }

    private async Task<ResultadoAcciones> Validar(E_NivelAcademico nivel, bool esUpdate = false)
    {
        var res = new ResultadoAcciones { Resultado = true };
        nivel.NombreNivelAcademico = Normalize(nivel.NombreNivelAcademico); // Se guarda normalizado

        if (string.IsNullOrWhiteSpace(nivel.NombreNivelAcademico))
        {
            res.Resultado = false;
            res.Mensajes.Add("El nombre del nivel académico es obligatorio.");
        }
        else if (await ExisteNombreNivelAcademicoInterno(nivel.NombreNivelAcademico, esUpdate ? nivel.IdNivelAcademico : null))
        {
            res.Resultado = false;
            res.Mensajes.Add("Ya existe un nivel académico con ese nombre.");
        }

        return res;
    }

    // ========================= CRUD (Sin cambios) =========================

    public async Task<ResultadoAcciones> InsertarNivelAcademico(E_NivelAcademico nivelAcademico)
    {
        try
        {
            var validacion = await Validar(nivelAcademico);
            if (!validacion.Resultado) return validacion;

            await _db.NivelesAcademicos.AddAsync(nivelAcademico);
            await _db.SaveChangesAsync();

            return new ResultadoAcciones { Resultado = true, Mensajes = { "Nivel académico insertado correctamente." } };
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { $"Error al insertar: {ex.Message}" } };
        }
    }

    public async Task<ResultadoAcciones> ModificarNivelAcademico(E_NivelAcademico nivelAcademico)
    {
        try
        {
            var existente = await _db.NivelesAcademicos.FindAsync(nivelAcademico.IdNivelAcademico);
            if (existente is null)
            {
                return new ResultadoAcciones { Resultado = false, Mensajes = { "Nivel académico no encontrado." } };
            }

            existente.NombreNivelAcademico = nivelAcademico.NombreNivelAcademico;

            var validacion = await Validar(existente, esUpdate: true);
            if (!validacion.Resultado) return validacion;

            await _db.SaveChangesAsync();
            return new ResultadoAcciones { Resultado = true, Mensajes = { "Nivel académico modificado correctamente." } };
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { $"Error al modificar: {ex.Message}" } };
        }
    }

    public async Task<ResultadoAcciones> BorrarNivelAcademico(int idNivelAcademico)
    {
        try
        {
            if (await EstaEnUso(idNivelAcademico))
            {
                return new ResultadoAcciones { Resultado = false, Mensajes = { "No se puede eliminar el nivel académico porque está asignado a uno o más planes de estudio." } };
            }

            var nivel = await _db.NivelesAcademicos.FindAsync(idNivelAcademico);
            if (nivel is null)
            {
                return new ResultadoAcciones { Resultado = false, Mensajes = { "Nivel académico no encontrado." } };
            }

            _db.NivelesAcademicos.Remove(nivel);
            await _db.SaveChangesAsync();
            return new ResultadoAcciones { Resultado = true, Mensajes = { "Nivel académico eliminado correctamente." } };
        }
        catch (Exception ex)
        {
            return new ResultadoAcciones { Resultado = false, Mensajes = { $"Error al eliminar: {ex.Message}" } };
        }
    }

    // ========================= Consultas (Sin cambios) =========================

    public async Task<E_NivelAcademico?> BuscarNivelAcademico(int idNivelAcademico)
        => await _db.NivelesAcademicos.AsNoTracking().FirstOrDefaultAsync(n => n.IdNivelAcademico == idNivelAcademico);

    public async Task<IEnumerable<E_NivelAcademico>> ListarNivelesAcademicos(string? criterioBusqueda = null)
    {
        var q = _db.NivelesAcademicos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(criterioBusqueda))
        {
            var term = Normalize(criterioBusqueda); // La búsqueda también se beneficia de la normalización
            q = q.Where(n => EF.Functions.Like(n.NombreNivelAcademico, $"%{term}%"));
        }

        return await q.ToListAsync();
    }

    // ========================= Validaciones (Sin cambios) =========================

    public async Task<bool> ExisteNombreNivelAcademico(string nombre, int? idExcluir = null)
        => await ExisteNombreNivelAcademicoInterno(nombre, idExcluir);

    public async Task<bool> EstaEnUso(int idNivelAcademico)
        => await _db.PlanEstudios.AnyAsync(p => p.IdNivelAcademico == idNivelAcademico);
}
