using Datos.Contexto;
using Datos.IRepositorios.PlanesDeEstudio;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Datos.Repositorios.PlanesDeEstudio;
public class DocenteRepositorio : IDocenteRepositorios
{
    private readonly ContextDB _context;
    
    public DocenteRepositorio(ContextDB context)
    {
        _context = context;
    }

    public async Task<E_Docentes?> BuscarDocente(int idDocente)
    {
        return await _context.Docentes.FindAsync(idDocente);
    }

    public async Task<ResultadoAcciones> InsertarDocente(E_Docentes docente)
    {
        try
        {
            _context.Docentes.Add(docente);
            await _context.SaveChangesAsync();
            return ResultadoAcciones.Exitoso("Docente insertado correctamente.");
        }
        catch (DbException ex)
        {
            return ResultadoAcciones.Fallido($"Error al insertar el docente: {ex.Message}");
        }
    }

    public async Task<ResultadoAcciones> ModificarDocente(E_Docentes docente)
    {
        try
        {
            _context.Entry(docente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ResultadoAcciones.Exitoso("Docente modificado correctamente.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return ResultadoAcciones.Fallido($"Error de concurrencia al modificar: El registro fue modificado por otro usuario. {ex.Message}");
        }
        catch (DbException ex)
        {
            return ResultadoAcciones.Fallido($"Error al modificar el docente: {ex.Message}");
        }
    }

    public async Task<ResultadoAcciones> BorrarDocente(int idDocente)
    {
        try
        {
            var docente = await _context.Docentes.FindAsync(idDocente);
            if (docente == null)
            {
                return ResultadoAcciones.Fallido("El docente que intenta eliminar no existe.");
            }

            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();
            return ResultadoAcciones.Exitoso("Docente eliminado correctamente.");
        }
        catch (DbException ex)
        {
            return ResultadoAcciones.Fallido($"Error al eliminar el docente. Es posible que esté asignado como coordinador. Detalle: {ex.Message}");
        }
    }

    public async Task<IEnumerable<E_Docentes>> ListarDocentes(string? criterioBusqueda = null)
    {
        var query = _context.Docentes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(criterioBusqueda))
        {
            var criterio = criterioBusqueda.ToLower().Trim();
            // Busca por nombre completo, número de empleado o email
            query = query.Where(d =>
                (d.NombreDocente + " " + d.PaternoDocente + " " + d.MaternoDocente).ToLower().Contains(criterio) ||
                d.NumeroEmpleado.ToLower().Contains(criterio) ||
                d.EmailAlterno.ToLower().Contains(criterio)
            );
        }

        return await query.OrderBy(d => d.PaternoDocente).ThenBy(d => d.MaternoDocente).ThenBy(d => d.NombreDocente).ToListAsync();
    }

    public async Task<bool> ExisteNumeroEmpleado(string numeroEmpleado, int? idDocenteExcluir = null)
    {
        var query = _context.Docentes.AsQueryable();
        if (idDocenteExcluir.HasValue)
        {
            query = query.Where(d => d.IdDocente != idDocenteExcluir.Value);
        }
        return await query.AnyAsync(d => d.NumeroEmpleado.ToLower() == numeroEmpleado.ToLower());
    }

    public async Task<bool> ExisteEmail(string email, int? idDocenteExcluir = null)
    {
        var query = _context.Docentes.AsQueryable();
        if (idDocenteExcluir.HasValue)
        {
            query = query.Where(d => d.IdDocente != idDocenteExcluir.Value);
        }
        return await query.AnyAsync(d => d.EmailAlterno.ToLower() == email.ToLower());
    }

    public async Task<bool> EsCoordinadorDeCarrera(int idDocente)
    {
        // Verifica si el docente está asignado como coordinador en alguna carrera activa.
        return await _context.Carreras.AnyAsync(c => c.IdCoordinador == idDocente && c.EstadoCarrera);
    }
}