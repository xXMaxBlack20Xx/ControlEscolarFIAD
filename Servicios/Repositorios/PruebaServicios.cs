using AutoMapper;
using Entidades.DTO;
using Negocios;
using Servicios.IRepositorios;

namespace Servicios.Repositorios;
public class PruebaServicios(PruebaNegocios negocio, IMapper mapper) : IPruebaServicios
{
    public async Task<List<PruebaDTO>> ListarAsync()
        => (await negocio.ListarAsync()).Select(e => mapper.Map<PruebaDTO>(e)).ToList();

    public async Task<(bool Ok, string? Error, PruebaDTO? Dto)> CrearAsync(string nombre)
    {
        var (ok, err, ent) = await negocio.CrearAsync(nombre);
        return ok ? (true, null, ent is null ? null : mapper.Map<PruebaDTO>(ent))
                  : (false, err, null);
    }
}