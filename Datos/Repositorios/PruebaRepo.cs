using Datos.Contexto;
using Datos.IRepositorios;
using Entidades.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositorios;

public class PruebaRepo(ContextDB db) : IPruebaRepo
{
    public Task<List<E_Prueba>> ListarAsync() => db.Pruebas.AsNoTracking().ToListAsync();
    public Task<E_Prueba?> ObtenerAsync(int id) => db.Pruebas.FindAsync(id).AsTask();
    public async Task<E_Prueba> InsertarAsync(E_Prueba entidad)
    {
        db.Pruebas.Add(entidad);
        await db.SaveChangesAsync();
        return entidad;
    }
}
