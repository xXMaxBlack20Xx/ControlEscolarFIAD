using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositorios.PlanesDeEstudio;

public class CarreraRepositorio(ContextDB db) : ICarreraRepositorios
{
    private readonly ContextDB _db = db;

    // ========================= Helpers =========================

    private static string N(string? s) => (s ?? string.Empty).Trim();
    private static string U(string? s) => (s ?? string.Empty).Trim().ToUpperInvariant();

    private async Task<bool> ExisteClaveCarrera(string clave, int? excluirId = null)
    {
        var q = _db.Carreras.AsQueryable();
        if (excluirId is not null) q = q.Where(c => c.IdCarrera != excluirId.Value);
        var claveN = U(clave);
        return await q.AnyAsync(c => c.ClaveCarrera == claveN);
    }

    private async Task<bool> ExisteNombreCarrera(string nombre, int? excluirId = null)
    {
        var q = _db.Carreras.AsQueryable();
        if (excluirId is not null) q = q.Where(c => c.IdCarrera != excluirId.Value);
        var nomN = N(nombre);
        return await q.AnyAsync(c => c.NombreCarrera == nomN);
    }

    private async Task<bool> ExisteAliasCarrera(string alias, int? excluirId = null)
    {
        var q = _db.Carreras.AsQueryable();
        if (excluirId is not null) q = q.Where(c => c.IdCarrera != excluirId.Value);
        var aliasN = N(alias);
        return await q.AnyAsync(c => c.AliasCarrera == aliasN);
    }

    private async Task<ResultadoAcciones> ValidarCarrera(E_Carrera carrera, bool esUpdate = false)
    {
        var res = new ResultadoAcciones { Resultado = true };

        // Normaliza antes de validar/comparar
        carrera.ClaveCarrera = U(carrera.ClaveCarrera);
        carrera.NombreCarrera = N(carrera.NombreCarrera);
        carrera.AliasCarrera = N(carrera.AliasCarrera);

        int? excluirId = esUpdate ? carrera.IdCarrera : null;

        // Clave (max 3)
        if (string.IsNullOrWhiteSpace(carrera.ClaveCarrera))
        {
            res.Mensajes.Add("La clave de la carrera es obligatoria.");
            res.Resultado = false;
        }
        else
        {
            if (carrera.ClaveCarrera.Length > 3)
            {
                res.Mensajes.Add("La clave de la carrera no debe exceder 3 caracteres.");
                res.Resultado = false;
            }
            if (await ExisteClaveCarrera(carrera.ClaveCarrera, excluirId))
            {
                res.Mensajes.Add("La clave de la carrera ya existe.");
                res.Resultado = false;
            }
        }

        // Nombre (max 50)
        if (string.IsNullOrWhiteSpace(carrera.NombreCarrera))
        {
            res.Mensajes.Add("El nombre de la carrera es obligatorio.");
            res.Resultado = false;
        }
        else
        {
            if (carrera.NombreCarrera.Length > 50)
            {
                res.Mensajes.Add("El nombre de la carrera no debe exceder 50 caracteres.");
                res.Resultado = false;
            }
            if (await ExisteNombreCarrera(carrera.NombreCarrera, excluirId))
            {
                res.Mensajes.Add("El nombre de la carrera ya existe.");
                res.Resultado = false;
            }
        }

        // Alias (max 50)
        if (string.IsNullOrWhiteSpace(carrera.AliasCarrera))
        {
            res.Mensajes.Add("El alias de la carrera es obligatorio.");
            res.Resultado = false;
        }
        else
        {
            if (carrera.AliasCarrera.Length > 50)
            {
                res.Mensajes.Add("El alias de la carrera no debe exceder 50 caracteres.");
                res.Resultado = false;
            }
            if (await ExisteAliasCarrera(carrera.AliasCarrera, excluirId))
            {
                res.Mensajes.Add("El alias de la carrera ya existe.");
                res.Resultado = false;
            }
        }

        return res;
    }

    // ========================= CRUD =========================

    public async Task<ResultadoAcciones> InsertarCarrera(E_Carrera carrera)
    {
        try
        {
            var res = await ValidarCarrera(carrera, esUpdate: false);
            if (!res.Resultado) return res;

            await _db.Carreras.AddAsync(carrera);
            await _db.SaveChangesAsync();

            res.Resultado = true;
            res.Mensajes.Add("Carrera insertada correctamente.");
            return res;
        }
        catch (Exception)
        {
            return new ResultadoAcciones
            {
                Resultado = false,
                Mensajes = { "Ocurrió un error inesperado al insertar la carrera." }
            };
        }
    }

    public async Task<ResultadoAcciones> ModificarCarrera(E_Carrera carrera)
    {
        try
        {
            var existente = await _db.Carreras.FirstOrDefaultAsync(c => c.IdCarrera == carrera.IdCarrera);
            if (existente is null)
            {
                return new ResultadoAcciones
                {
                    Resultado = false,
                    Mensajes = { $"La carrera con ID {carrera.IdCarrera} no fue encontrada." }
                };
            }

            // Copia campos a actualizar + normaliza
            existente.ClaveCarrera = U(carrera.ClaveCarrera);
            existente.NombreCarrera = N(carrera.NombreCarrera);
            existente.AliasCarrera = N(carrera.AliasCarrera);
            existente.IdCoordinador = carrera.IdCoordinador;
            existente.EstadoCarrera = carrera.EstadoCarrera;

            var res = await ValidarCarrera(existente, esUpdate: true);
            if (!res.Resultado) return res;

            await _db.SaveChangesAsync();

            res.Resultado = true;
            res.Mensajes.Add("Carrera modificada correctamente.");
            return res;
        }
        catch (Exception)
        {
            return new ResultadoAcciones
            {
                Resultado = false,
                Mensajes = { "Ocurrió un error inesperado al modificar la carrera." }
            };
        }
    }

    public async Task<ResultadoAcciones> BorrarCarrera(int idCarrera)
    {
        try
        {
            var carrera = await _db.Carreras.FindAsync(idCarrera);
            if (carrera is null)
            {
                return new ResultadoAcciones
                {
                    Resultado = false,
                    Mensajes = { "No se encontró la carrera." }
                };
            }

            _db.Carreras.Remove(carrera);
            await _db.SaveChangesAsync();

            return new ResultadoAcciones
            {
                Resultado = true,
                Mensajes = { "Carrera eliminada correctamente." }
            };
        }
        catch (Exception)
        {
            return new ResultadoAcciones
            {
                Resultado = false,
                Mensajes = { "Ocurrió un error inesperado al eliminar la carrera." }
            };
        }
    }

    // ========================= Consultas =========================

    public async Task<E_Carrera?> BuscarCarrera(int idCarrera)
        => await _db.Carreras.AsNoTracking().FirstOrDefaultAsync(c => c.IdCarrera == idCarrera);

    public async Task<IEnumerable<E_Carrera>> ListarCarreras(string? criterioBusqueda = null)
    {
        var q = _db.Carreras.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(criterioBusqueda))
        {
            var term = criterioBusqueda.Trim();
            q = q.Where(c =>
                EF.Functions.Like(c.ClaveCarrera, $"%{term}%") ||
                EF.Functions.Like(c.NombreCarrera, $"%{term}%") ||
                EF.Functions.Like(c.AliasCarrera, $"%{term}%"));
        }

        return await q.ToListAsync();
    }

    public async Task<IEnumerable<E_Carrera>> ListarCarrerasPorPlanEstudio(int idPlanEstudio)
    {
        return await _db.Carreras
            .AsNoTracking()
            .Where(c => c.PlanEstudios.Any(p => p.IdPlanEstudio == idPlanEstudio))
            .ToListAsync();
    }

    // ========================= Validaciones públicas (si la interfaz las expone) =========================

    public async Task<bool> ExisteClaveCarrera(string clave)
        => await ExisteClaveCarrera(clave, excluirId: null);

    public async Task<bool> ExisteNombreCarrera(string nombre)
        => await ExisteNombreCarrera(nombre, excluirId: null);

    public async Task<bool> ExisteAliasCarrera(string alias)
        => await ExisteAliasCarrera(alias, excluirId: null);
}