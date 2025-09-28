using Datos.IRepositorios;
using Entidades.Modelos;

namespace Negocios;
public class PruebaNegocios(IPruebaRepo repo)
{
    public Task<List<E_Prueba>> ListarAsync() => repo.ListarAsync();

    public async Task<(bool Ok, string? Error, E_Prueba? Entidad)> CrearAsync(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return (false, "El nombre es obligatorio.", null);

        var entidad = new E_Prueba { Nombre = nombre.Trim() };
        entidad = await repo.InsertarAsync(entidad);
        return (true, null, entidad);
    }
}