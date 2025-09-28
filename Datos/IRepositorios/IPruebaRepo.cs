using Entidades.Modelos;

namespace Datos.IRepositorios;

public interface IPruebaRepo
{
    Task<List<E_Prueba>> ListarAsync();
    Task<E_Prueba?> ObtenerAsync(int id);
    Task<E_Prueba> InsertarAsync(E_Prueba entidad);
}