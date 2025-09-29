using AutoMapper;
using Entidades.DTO.PlanesDeEstudio.Carreras;
using Entidades.Generales;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Negocios.Repositorios.PlanesDeEstudio;
using Servicios.IRepositorios.PlanesDeEstudio;

namespace Servicios.Repositorios.PlanesDeEstudio;

public class CarreraServicios : ICarreraServicios
{
    private readonly CarreraNegocios _carreraNegocios;
    private readonly IMapper _mapper;

    public CarreraServicios(CarreraNegocios carreraNegocios, IMapper mapper)
    {
        _carreraNegocios = carreraNegocios;
        _mapper = mapper;
    }

    public async Task<ResultadoAcciones> InsertarCarrera(CarreraDTO carreraDTO)
    {
        if (carreraDTO is null)
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Los datos de la carrera son requeridos." } };

        Normalizar(carreraDTO);

        var entidad = _mapper.Map<E_Carrera>(carreraDTO);
        return await _carreraNegocios.InsertarCarrera(entidad);
    }

    public async Task<ResultadoAcciones> BorrarCarrera(int idCarrera)
    {
        if (idCarrera <= 0)
            return new ResultadoAcciones { Resultado = false, Mensajes = { "El identificador de la carrera no es válido." } };

        return await _carreraNegocios.BorrarCarrera(idCarrera);
    }

    public async Task<ResultadoAcciones> ModificarCarrera(CarreraDTO carreraDTO)
    {
        if (carreraDTO is null || carreraDTO.IdCarrera <= 0)
            return new ResultadoAcciones { Resultado = false, Mensajes = { "Los datos de la carrera son inválidos." } };

        Normalizar(carreraDTO);

        var entidad = _mapper.Map<E_Carrera>(carreraDTO);
        return await _carreraNegocios.ModificarCarrera(entidad);
    }

    public async Task<ResultadoAccion<T>> ObtenerCarrera<T>(int idCarrera) where T : class
    {
        var r = await _carreraNegocios.ObtenerCarreraPorId(idCarrera);
        if (!r.Resultado || r.Entidad is null)
            return new ResultadoAccion<T> { Resultado = false, Mensajes = r.Mensajes, Entidad = default };

        var dto = _mapper.Map<T>(r.Entidad);
        return new ResultadoAccion<T> { Resultado = true, Mensajes = r.Mensajes, Entidad = dto };
    }

    // ===== Lista en DTOs (como marca la interfaz) =====
    async Task<IEnumerable<CarreraDTO>> ICarreraServicios.ListarCarreras(string? criterioBusqueda)
    {
        var entidades = await _carreraNegocios.ListarCarreras(criterioBusqueda);
        return _mapper.Map<IEnumerable<CarreraDTO>>(entidades);
    }

    public async Task<List<CarreraDTO>> ObtenerCarrerasPorPlanEstudioAsync(int idPlanEstudio)
    {
        if (idPlanEstudio <= 0) return new List<CarreraDTO>();

        var entidades = await _carreraNegocios.ListarCarrerasPorPlanEstudio(idPlanEstudio);
        return _mapper.Map<List<CarreraDTO>>(entidades);
    }

    // ---------- helpers ----------
    private static void Normalizar(CarreraDTO d)
    {
        d.ClaveCarrera = (d.ClaveCarrera ?? string.Empty).Trim().ToUpperInvariant();
        d.NombreCarrera = (d.NombreCarrera ?? string.Empty).Trim();
        d.AliasCarrera = (d.AliasCarrera ?? string.Empty).Trim();
    }
}