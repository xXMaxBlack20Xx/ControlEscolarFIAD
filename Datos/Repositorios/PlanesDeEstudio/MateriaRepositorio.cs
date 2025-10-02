using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositorios.PlanesDeEstudio;

public class MateriaRepositorio(ContextDB db) : IMateriaRepositorio
{
    private readonly ContextDB _db = db;

    // ========================= Helpers =========================

    private static string N(string? s) => (s ?? string.Empty).Trim().ToUpperInvariant();

    private async Task<bool> ExisteMateriaInterno(string claveMateria, int? excluirId = null)
    {
        var q = _db.Materias.AsQueryable();
        if (excluirId is not null)
            q = q.Where(m => m.IdMateria != excluirId.Value);

        var claveN = N(claveMateria);
        return await q.AnyAsync(m => m.ClaveMateria == claveN);
    }

    private async Task<ResultadoAcciones> ValidarMateria(E_Materias materia, bool esUpdate = false)
    {
        var res = new ResultadoAcciones { Resultado = true };
        materia.ClaveMateria = N(materia.ClaveMateria);

        int? excluirId = esUpdate ? materia.IdMateria : null;

        if (string.IsNullOrWhiteSpace(materia.ClaveMateria))
        {
            res.Mensajes.Add("La clave de la materia es obligatoria.");
            res.Resultado = false;
        }
        else if (await ExisteMateriaInterno(materia.ClaveMateria, excluirId))
        {
            res.Mensajes.Add($"Ya existe una materia con la clave '{materia.ClaveMateria}'.");
            res.Resultado = false;
        }

        if (materia.HC + materia.HL + materia.HT != materia.HPC)
        {
            res.Mensajes.Add("La suma de horas (Clase + Laboratorio + Taller) debe ser igual a las Horas por Ciclo.");
            res.Resultado = false;
        }

        return res;
    }

    // ========================= CRUD =========================

    public async Task<ResultadoAcciones> InsertarMateria(E_Materias materia)
    {
        try
        {
            var res = await ValidarMateria(materia, esUpdate: false);
            if (!res.Resultado) return res;

            await _db.Materias.AddAsync(materia);
            await _db.SaveChangesAsync();

            res.Mensajes.Add("Materia insertada correctamente.");
            return res;
        }
        catch (Exception ex)
        {
            return ResultadoAcciones.Fallido($"Error al insertar materia: {ex.Message}");
        }
    }

    public async Task<ResultadoAcciones> ModificarMateria(E_Materias materia)
    {
        try
        {
            var existente = await _db.Materias.FindAsync(materia.IdMateria);
            if (existente is null)
            {
                return ResultadoAcciones.Fallido($"La materia con ID {materia.IdMateria} no fue encontrada.");
            }

            // Copiar todas las propiedades
            _db.Entry(existente).CurrentValues.SetValues(materia);

            var res = await ValidarMateria(existente, esUpdate: true);
            if (!res.Resultado) return res;

            await _db.SaveChangesAsync();

            res.Mensajes.Add("Materia modificada correctamente.");
            return res;
        }
        catch (Exception ex)
        {
            return ResultadoAcciones.Fallido($"Error al modificar materia: {ex.Message}");
        }
    }

    public async Task<ResultadoAcciones> BorrarMateria(int idMateria)
    {
        try
        {
            var materia = await _db.Materias.FindAsync(idMateria);
            if (materia is null)
            {
                return ResultadoAcciones.Fallido("No se encontró la materia.");
            }

            _db.Materias.Remove(materia);
            await _db.SaveChangesAsync();

            return ResultadoAcciones.Exitoso("Materia eliminada correctamente.");
        }
        catch (Exception ex)
        {
            return ResultadoAcciones.Fallido($"Error al eliminar materia: {ex.Message}");
        }
    }

    // ========================= Consultas =========================

    public async Task<E_Materias?> BuscarMateria(int idMateria)
        => await _db.Materias.AsNoTracking().FirstOrDefaultAsync(m => m.IdMateria == idMateria);

    public async Task<IEnumerable<E_Materias>> ListarMaterias(string? criterioBusqueda = null)
    {
        var q = _db.Materias.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(criterioBusqueda))
        {
            var term = criterioBusqueda.Trim();
            q = q.Where(m =>
                EF.Functions.Like(m.ClaveMateria, $"%{term}%") ||
                EF.Functions.Like(m.NombreMateria, $"%{term}%"));
        }

        return await q.OrderBy(m => m.NombreMateria).ToListAsync();
    }

    // ========================= Validaciones =========================

    public async Task<bool> ExisteMateria(string claveMateria) => await ExisteMateriaInterno(claveMateria);
}