using Entidades.DTO;

namespace Servicios.IRepositorios;
public interface IPruebaServicios
{
    Task<List<PruebaDTO>> ListarAsync();
    Task<(bool Ok, string? Error, PruebaDTO? Dto)> CrearAsync(string nombre);
}